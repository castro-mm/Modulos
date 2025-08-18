using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;

namespace Contas.Core.Extensions;

public static class LogDeErroMappingExtensions
{
    public static LogDeErroDto ToDto(this LogDeErro logDeErro)
    {
        ArgumentNullException.ThrowIfNull(logDeErro);

        return new LogDeErroDto
        {
            Id = logDeErro.Id,
            Mensagem = logDeErro.Mensagem,
            Detalhes = logDeErro.Detalhes,
            Metodo = logDeErro.Metodo,
            Caminho = logDeErro.Caminho,
            Ip = logDeErro.Ip,
            Navegador = logDeErro.Navegador,
            DataDeCriacao = logDeErro.DataDeCriacao,
            DataDeAtualizacao = logDeErro.DataDeAtualizacao,
            Usuario = logDeErro.Usuario,
            TraceId = logDeErro.TraceId
        };
    }

    public static LogDeErro ToEntity(this LogDeErroDto logDeErroDto)
    {
        ArgumentNullException.ThrowIfNull(logDeErroDto);

        return new LogDeErro
        {
            Id = logDeErroDto.Id,
            Mensagem = logDeErroDto.Mensagem,
            Detalhes = logDeErroDto.Detalhes,
            Metodo = logDeErroDto.Metodo,
            Caminho = logDeErroDto.Caminho,
            Ip = logDeErroDto.Ip,
            Navegador = logDeErroDto.Navegador,
            DataDeCriacao = logDeErroDto.DataDeCriacao,
            DataDeAtualizacao = logDeErroDto.DataDeAtualizacao,
            Usuario = logDeErroDto.Usuario,
            TraceId = logDeErroDto.TraceId,
            Hash = logDeErroDto.Hash
        };
    }

    public static void FromDto(this LogDeErro logDeErro, LogDeErroDto logDeErroDto)
    {
        ArgumentNullException.ThrowIfNull(logDeErroDto);
        ArgumentNullException.ThrowIfNull(logDeErro);

        logDeErro.Id = logDeErroDto.Id;
        logDeErro.Mensagem = logDeErroDto.Mensagem;
        logDeErro.Detalhes = logDeErroDto.Detalhes;
        logDeErro.Metodo = logDeErroDto.Metodo;
        logDeErro.Caminho = logDeErroDto.Caminho;
        logDeErro.Ip = logDeErroDto.Ip;
        logDeErro.Navegador = logDeErroDto.Navegador;
        logDeErro.DataDeCriacao = logDeErroDto.DataDeCriacao;
        logDeErro.DataDeAtualizacao = logDeErroDto.DataDeAtualizacao;
        logDeErro.Usuario = logDeErroDto.Usuario;
        logDeErro.TraceId = logDeErroDto.TraceId;
        logDeErro.Hash = logDeErroDto.Hash;
    }
}
