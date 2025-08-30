using Contas.Core.Dtos;
using Contas.Core.Entities;

namespace Contas.Core.Extensions;

public static class RegistroDaContaMappingExtensions
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
            Descricao = registroDaConta.Descricao,
            DataDeVencimento = registroDaConta.DataDeVencimento,
            CodigoDeBarras = registroDaConta.CodigoDeBarras,
            DataDePagamento = registroDaConta.DataDePagamento,
            Valor = registroDaConta.Valor,
            ValorDosJuros = registroDaConta.ValorDosJuros,
            ValorPago = registroDaConta.ValorPago,
            ValorDoDesconto = registroDaConta.ValorDoDesconto,
            Observacoes = registroDaConta.Observacoes,
            Status = registroDaConta.Status,
            DataDeCriacao = registroDaConta.DataDeCriacao,
            DataDeAtualizacao = registroDaConta.DataDeAtualizacao,
            Credor = registroDaConta.Credor?.ToDto(),
            Pagador = registroDaConta.Pagador?.ToDto(),
            Arquivos = registroDaConta.Arquivos?.Select(a => a.ToDto()).ToList() ?? [],
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
            Descricao = dto.Descricao,
            DataDeVencimento = dto.DataDeVencimento,
            CodigoDeBarras = dto.CodigoDeBarras,
            DataDePagamento = dto.DataDePagamento,
            Valor = dto.Valor,
            ValorDosJuros = dto.ValorDosJuros,
            ValorPago = dto.ValorPago,
            ValorDoDesconto = dto.ValorDoDesconto,
            Observacoes = dto.Observacoes,
            Status = dto.Status,
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
        registroDaConta.Descricao = dto.Descricao;
        registroDaConta.DataDeVencimento = dto.DataDeVencimento;
        registroDaConta.CodigoDeBarras = dto.CodigoDeBarras;
        registroDaConta.DataDePagamento = dto.DataDePagamento;
        registroDaConta.Valor = dto.Valor;
        registroDaConta.ValorDosJuros = dto.ValorDosJuros;
        registroDaConta.ValorPago = dto.ValorPago;
        registroDaConta.ValorDoDesconto = dto.ValorDoDesconto;
        registroDaConta.Observacoes = dto.Observacoes;
        registroDaConta.Status = dto.Status;
        registroDaConta.DataDeAtualizacao = dto.DataDeAtualizacao;
    }
}
