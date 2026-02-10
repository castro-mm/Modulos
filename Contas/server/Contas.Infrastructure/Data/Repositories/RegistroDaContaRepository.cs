using Contas.Core.Dtos.Dahsboard;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data.Repositories;

public class RegistroDaContaRepository : Repository<RegistroDaConta>, IRegistroDaContaRepository
{
    private readonly ContasContext _context;

    public RegistroDaContaRepository(ContasContext context) : base(context)
    {
        _context = context;
    }

    public async Task<QuantitativoDeContasDto> ObterQuantitativoDeContasAsync(CancellationToken cancellationToken)
    {
        var hoje = DateTime.Now.Date;

        // Busca os dados resumidos do banco (apenas o necessário)
        var contas = await _context.Set<RegistroDaConta>()
            .AsNoTracking()
            .Select(x => new 
            {
                x.Valor,
                x.DataDeVencimento,
                x.DataDePagamento
            })
            .ToListAsync(cancellationToken);

        if (contas.Count == 0)
            return new QuantitativoDeContasDto();

        // Classifica as contas em memória
        var contasPagas = contas.Where(x => x.DataDePagamento.HasValue).ToList();
        var contasVencidas = contas.Where(x => !x.DataDePagamento.HasValue && x.DataDeVencimento.Date < hoje).ToList();
        var contasAbertas = contas.Where(x => !x.DataDePagamento.HasValue && x.DataDeVencimento.Date >= hoje).ToList();
        var contasQueVencemHoje = contas.Where(x => !x.DataDePagamento.HasValue && x.DataDeVencimento.Date == hoje).ToList();

        var totalContas = contas.Count;

        var quantitativoDeContas = new QuantitativoDeContasDto
        {
            ContasPagas = CriarSumario([.. contasPagas.Select(x => x.Valor)], totalContas),
            ContasVencidas = CriarSumario([.. contasVencidas.Select(x => x.Valor)], totalContas),
            ContasAbertas = CriarSumario([.. contasAbertas.Select(x => x.Valor)], totalContas),
            ContasQueVencemHoje = CriarSumario([.. contasQueVencemHoje.Select(x => x.Valor)], totalContas)
        };

        return quantitativoDeContas;
    }

    private static SumarioDasContas CriarSumario(List<decimal> valores, int totalContas)
    {
        var quantidade = valores.Count;
        var valorTotal = valores.Sum();

        return new SumarioDasContas
        {
            Quantidade = quantidade,
            Valor = valorTotal,
            Percentual = totalContas > 0 ? (quantidade / (decimal)totalContas) * 100 : 0,
            ValorMedio = quantidade > 0 ? valorTotal / quantidade : 0
        };
    }
}
