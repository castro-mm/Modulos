using Contas.Core.Dtos;
using Contas.Core.Entities;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Core.Mappings;

public static class RegistroDaContaMappings
{
    public static RegistroDaContaDto ToDto(this RegistroDaConta registroDaConta)
    {
        ArgumentNullException.ThrowIfNull(registroDaConta);

        return new RegistroDaContaDto
        {
            Id = registroDaConta.Id,
            CredorId = registroDaConta.CredorId,
            PagadorId = registroDaConta.PagadorId,
            Mes = registroDaConta.Mes,
            Ano = registroDaConta.Ano,
            Periodo = $"{GetMonthName(registroDaConta.Mes)} de {registroDaConta.Ano}",
            DataDeVencimento = registroDaConta.DataDeVencimento,
            CodigoDeBarras = registroDaConta.CodigoDeBarras,
            DataDePagamento = registroDaConta.DataDePagamento,
            Valor = registroDaConta.Valor,
            ValorDosJuros = registroDaConta.ValorDosJuros,
            ValorTotal = registroDaConta.ValorTotal,
            ValorDoDesconto = registroDaConta.ValorDoDesconto,
            Observacoes = registroDaConta.Observacoes,
            DataDeCriacao = registroDaConta.DataDeCriacao,
            DataDeAtualizacao = registroDaConta.DataDeAtualizacao,
            Credor = registroDaConta.Credor?.ToDto(),
            Pagador = registroDaConta.Pagador?.ToDto(),
            Arquivos = registroDaConta.ArquivosDoRegistroDaConta?.Select(a => a.ToDto()).ToList() ?? [],
            Status = registroDaConta.DataDePagamento.HasValue 
                ? StatusDaConta.Paga 
                : registroDaConta.DataDeVencimento < DateTime.Now.AddDays(-1) 
                    ? StatusDaConta.Vencida 
                    : StatusDaConta.Pendente,
            DiasParaVencer = registroDaConta.DataDePagamento.HasValue 
                ? 0
                : registroDaConta.DataDeVencimento > DateTime.Now
                    ? registroDaConta.DataDeVencimento.Subtract(DateTime.Now).Days 
                    : 0,
            DiasEmAtraso = registroDaConta.DataDePagamento.HasValue 
                ? 0 
                : registroDaConta.DataDeVencimento < DateTime.Now
                    ? DateTime.Now.Subtract(registroDaConta.DataDeVencimento).Days 
                    : 0            
        };
    }

    public static RegistroDaConta ToEntity(this RegistroDaContaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new RegistroDaConta
        {
            Id = dto.Id,
            CredorId = dto.CredorId,
            PagadorId = dto.PagadorId,
            Mes = dto.Mes,
            Ano = dto.Ano,
            DataDeVencimento = dto.DataDeVencimento,
            CodigoDeBarras = dto.CodigoDeBarras,
            DataDePagamento = dto.DataDePagamento,
            Valor = dto.Valor,
            ValorDosJuros = dto.ValorDosJuros,
            ValorTotal = dto.ValorTotal,
            ValorDoDesconto = dto.ValorDoDesconto,
            Observacoes = dto.Observacoes,
            DataDeCriacao = dto.DataDeCriacao,
            DataDeAtualizacao = dto.DataDeAtualizacao,
            Credor = null!,
            Pagador = null!                     
        };
    }

    public static void FromDto(this RegistroDaConta registroDaConta, RegistroDaContaDto dto)
    {
        ArgumentNullException.ThrowIfNull(registroDaConta);
        ArgumentNullException.ThrowIfNull(dto);

        registroDaConta.CredorId = dto.CredorId;
        registroDaConta.PagadorId = dto.PagadorId;
        registroDaConta.Mes = dto.Mes;
        registroDaConta.Ano = dto.Ano;
        registroDaConta.DataDeVencimento = dto.DataDeVencimento;
        registroDaConta.CodigoDeBarras = dto.CodigoDeBarras;
        registroDaConta.DataDePagamento = dto.DataDePagamento;
        registroDaConta.Valor = dto.Valor;
        registroDaConta.ValorDosJuros = dto.ValorDosJuros;
        registroDaConta.ValorTotal = dto.ValorTotal;
        registroDaConta.ValorDoDesconto = dto.ValorDoDesconto;
        registroDaConta.Observacoes = dto.Observacoes;
        registroDaConta.DataDeAtualizacao = dto.DataDeAtualizacao;
    }

    private static string GetMonthName(int month)
    {
        return month switch
        {
            1 => "Janeiro",
            2 => "Fevereiro",
            3 => "Março",
            4 => "Abril",
            5 => "Maio",
            6 => "Junho",
            7 => "Julho",
            8 => "Agosto",
            9 => "Setembro",
            10 => "Outubro",
            11 => "Novembro",
            12 => "Dezembro",
            _ => throw new ArgumentOutOfRangeException(nameof(month), "Mês inválido")
        };
    }
}
