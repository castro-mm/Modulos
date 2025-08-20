using Contas.Core.Dtos.System;
using Contas.Core.Entities.System;

namespace Contas.Core.Extensions;

public static class TrilhaDeAuditoriaExtensions
{
    public static TrilhaDeAuditoriaDto ToDto(this TrilhaDeAuditoria trilhaDeAuditoria)
    {
        ArgumentNullException.ThrowIfNull(trilhaDeAuditoria);

        return new TrilhaDeAuditoriaDto
        {
            Id = trilhaDeAuditoria.Id,
            Entidade = trilhaDeAuditoria.Entidade,
            Metodo = trilhaDeAuditoria.Metodo,
            Caminho = trilhaDeAuditoria.Caminho,
            Operacao = trilhaDeAuditoria.Operacao,
            ValoresAntigos = trilhaDeAuditoria.ValoresAntigos,
            ValoresNovos = trilhaDeAuditoria.ValoresNovos,
            Ip = trilhaDeAuditoria.Ip,
            Navegador = trilhaDeAuditoria.Navegador,
            Usuario = trilhaDeAuditoria.Usuario,
            TraceId = trilhaDeAuditoria.TraceId,
            Hash = trilhaDeAuditoria.Hash
        };
    }

    public static TrilhaDeAuditoria ToEntity(this TrilhaDeAuditoriaDto trilhaDeAuditoriaDto)
    {
        ArgumentNullException.ThrowIfNull(trilhaDeAuditoriaDto);

        return new TrilhaDeAuditoria
        {
            Id = trilhaDeAuditoriaDto.Id,            
            Entidade = trilhaDeAuditoriaDto.Entidade,
            Metodo = trilhaDeAuditoriaDto.Metodo,
            Caminho = trilhaDeAuditoriaDto.Caminho,
            Operacao = trilhaDeAuditoriaDto.Operacao,
            ValoresAntigos = trilhaDeAuditoriaDto.ValoresAntigos,
            ValoresNovos = trilhaDeAuditoriaDto.ValoresNovos,
            Ip = trilhaDeAuditoriaDto.Ip,
            Navegador = trilhaDeAuditoriaDto.Navegador,
            Usuario = trilhaDeAuditoriaDto.Usuario,
            TraceId = trilhaDeAuditoriaDto.TraceId,
            Hash = trilhaDeAuditoriaDto.Hash
        };
    }

    public static void FromDto(this TrilhaDeAuditoria trilhaDeAuditoria, TrilhaDeAuditoriaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
        ArgumentNullException.ThrowIfNull(trilhaDeAuditoria);

        trilhaDeAuditoria.Entidade = dto.Entidade;
        trilhaDeAuditoria.Metodo = dto.Metodo;
        trilhaDeAuditoria.Caminho = dto.Caminho;
        trilhaDeAuditoria.Operacao = dto.Operacao;
        trilhaDeAuditoria.ValoresAntigos = dto.ValoresAntigos;
        trilhaDeAuditoria.ValoresNovos = dto.ValoresNovos;
        trilhaDeAuditoria.Ip = dto.Ip;
        trilhaDeAuditoria.Navegador = dto.Navegador;
        trilhaDeAuditoria.Usuario = dto.Usuario;
        trilhaDeAuditoria.TraceId = dto.TraceId;
        trilhaDeAuditoria.Hash = dto.Hash;
    }
}
