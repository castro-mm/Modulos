using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;

namespace Contas.Core.Mappings;

public static class ArquivoMappings
{
    public static Arquivo ToEntity(this ArquivoDto arquivoDto)
    {
        ArgumentNullException.ThrowIfNull(arquivoDto);

        return new Arquivo
        {
            Id = arquivoDto.Id,
            Nome = arquivoDto.Nome,
            Extensao = arquivoDto.Extensao,
            Tamanho = arquivoDto.Tamanho,
            Tipo = arquivoDto.Tipo,
            Dados = arquivoDto.Dados,
            DataDaUltimaModificacao = arquivoDto.DataDaUltimaModificacao,
            DataDeCriacao = arquivoDto.DataDeCriacao,
            DataDeAtualizacao = arquivoDto.DataDeAtualizacao
        };
    }

    public static ArquivoDto ToDto(this Arquivo arquivo)
    {
        ArgumentNullException.ThrowIfNull(arquivo);

        return new ArquivoDto
        {
            Id = arquivo.Id,
            Nome = arquivo.Nome,
            Extensao = arquivo.Extensao,
            Tamanho = arquivo.Tamanho,
            Tipo = arquivo.Tipo,
            Dados = arquivo.Dados,
            DataDaUltimaModificacao = arquivo.DataDaUltimaModificacao,
            DataDeCriacao = arquivo.DataDeCriacao,
            DataDeAtualizacao = arquivo.DataDeAtualizacao,
        };
    }

    public static void FromDto(this Arquivo arquivo, ArquivoDto arquivoDto)
    {
        ArgumentNullException.ThrowIfNull(arquivo);
        ArgumentNullException.ThrowIfNull(arquivoDto);

        arquivo.Id = arquivoDto.Id;
        arquivo.Nome = arquivoDto.Nome;
        arquivo.Extensao = arquivoDto.Extensao;
        arquivo.Tamanho = arquivoDto.Tamanho;
        arquivo.Tipo = arquivoDto.Tipo;
        arquivo.Dados = arquivoDto.Dados;
        arquivo.DataDaUltimaModificacao = arquivoDto.DataDaUltimaModificacao;
        arquivo.DataDeAtualizacao = arquivoDto.DataDeAtualizacao;
    }    
}
