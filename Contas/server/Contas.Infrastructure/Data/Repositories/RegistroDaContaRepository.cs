using Contas.Core.Dtos.Dahsboard;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contas.Core.Data.Repositories;

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

    public async Task<GastoMensalPorCredorDto> ObterGastoMensalPorCredorAsync(int ano, CancellationToken cancellationToken)
    {
        // Obtém os anos disponíveis para o filtro
        var anosDisponiveis = await _context.Set<RegistroDaConta>()
            .AsNoTracking()
            .Select(x => x.Ano)
            .Distinct()
            .OrderByDescending(x => x)
            .ToListAsync(cancellationToken);

        // Busca os dados agrupados por credor e mês para o ano selecionado
        var dados = await _context.Set<RegistroDaConta>()
            .AsNoTracking()
            .Where(x => x.Ano == ano)
            .GroupBy(x => new { x.CredorId, x.Credor.NomeFantasia, x.Mes })
            .Select(g => new
            {
                g.Key.CredorId,
                g.Key.NomeFantasia,
                g.Key.Mes,
                ValorTotal = g.Sum(x => x.ValorTotal)
            })
            .ToListAsync(cancellationToken);

        // Agrupa por credor e distribui os valores nos 12 meses
        var credores = dados
            .GroupBy(x => new { x.CredorId, x.NomeFantasia })
            .Select(g =>
            {
                var valores = new decimal[12];
                foreach (var item in g)
                {
                    if (item.Mes >= 1 && item.Mes <= 12)
                        valores[item.Mes - 1] = item.ValorTotal;
                }

                return new CredorGastoMensalDto
                {
                    CredorId = g.Key.CredorId,
                    NomeFantasia = g.Key.NomeFantasia,
                    Valores = valores
                };
            })
            .OrderBy(x => x.NomeFantasia)
            .ToList();

        return new GastoMensalPorCredorDto
        {
            Ano = ano,
            AnosDisponiveis = anosDisponiveis,
            Credores = credores
        };
    }

    public async Task<GastoPorSegmentoDoCredorDto> ObterGastoPorSegmentoDoCredorAsync(int ano, CancellationToken cancellationToken)
    {
        var anosDisponiveis = await _context.Set<RegistroDaConta>()
            .AsNoTracking()
            .Select(x => x.Ano)
            .Distinct()
            .OrderByDescending(x => x)
            .ToListAsync(cancellationToken);

        var segmentos = await _context.Set<RegistroDaConta>()
            .AsNoTracking()
            .Where(x => x.Ano == ano)
            .GroupBy(x => new { x.Credor.SegmentoDoCredorId, x.Credor.SegmentoDoCredor.Nome })
            .Select(g => new SegmentoGastoDto
            {
                SegmentoDoCredorId = g.Key.SegmentoDoCredorId,
                Nome = g.Key.Nome,
                ValorTotal = g.Sum(x => x.ValorTotal)
            })
            .OrderByDescending(x => x.ValorTotal)
            .ToListAsync(cancellationToken);

        return new GastoPorSegmentoDoCredorDto
        {
            Ano = ano,
            AnosDisponiveis = anosDisponiveis,
            Segmentos = segmentos
        };
    }
}
