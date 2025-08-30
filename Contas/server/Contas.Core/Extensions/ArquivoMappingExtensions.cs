using Contas.Core.Dtos;
using Contas.Core.Entities;

namespace Contas.Core.Extensions;

public static class ArquivoMappingExtensions
{
    public static Arquivo ToEntity(this ArquivoDto arquivoDto)
    {
        ArgumentNullException.ThrowIfNull(arquivoDto);

        return new Arquivo
        {
            Id = arquivoDto.Id,
            RegistroDaContaId = arquivoDto.RegistroDaContaId,
            Nome = arquivoDto.Nome,
            Extensao = arquivoDto.Extensao,
            Tamanho = arquivoDto.Tamanho,
            Conteudo = arquivoDto.Conteudo,
            Tipo = arquivoDto.Tipo,
            DataDeCriacao = arquivoDto.DataDeCriacao,
            DataDeAtualizacao = arquivoDto.DataDeAtualizacao,
            RegistroDaConta = null! 
        };
    }

    public static ArquivoDto ToDto(this Arquivo arquivo)
    {
        ArgumentNullException.ThrowIfNull(arquivo);

        return new ArquivoDto
        {
            Id = arquivo.Id,
            RegistroDaContaId = arquivo.RegistroDaContaId,
            Nome = arquivo.Nome,
            Extensao = arquivo.Extensao,
            Tamanho = arquivo.Tamanho,
            Conteudo = arquivo.Conteudo,
            Tipo = arquivo.Tipo,
            DataDeCriacao = arquivo.DataDeCriacao,
            DataDeAtualizacao = arquivo.DataDeAtualizacao
        };
    }

    public static void FromDto(this Arquivo arquivo, ArquivoDto arquivoDto)
    {
        ArgumentNullException.ThrowIfNull(arquivo);
        ArgumentNullException.ThrowIfNull(arquivoDto);

        arquivo.Id = arquivoDto.Id;
        arquivo.RegistroDaContaId = arquivoDto.RegistroDaContaId;
        arquivo.Nome = arquivoDto.Nome;
        arquivo.Extensao = arquivoDto.Extensao;
        arquivo.Tamanho = arquivoDto.Tamanho;
        arquivo.Conteudo = arquivoDto.Conteudo;
        arquivo.Tipo = arquivoDto.Tipo;
        arquivo.DataDeAtualizacao = arquivoDto.DataDeAtualizacao;
    }
}
