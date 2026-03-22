using Contas.Core.Dtos;
using Contas.Core.Entities;

namespace Contas.Core.Mappings;

public static class ArquivoDoRegistroDaContaMappings
{
    public static ArquivoDoRegistroDaConta ToEntity(this ArquivoDoRegistroDaContaDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);
    
        return new ArquivoDoRegistroDaConta
        {
            Id = dto.Id,
            RegistroDaContaId = dto.RegistroDaContaId,
            ArquivoId = dto.ArquivoId,
            ModalidadeDoArquivo = dto.ModalidadeDoArquivo,
            DataDeCriacao = dto.DataDeCriacao,
            DataDeAtualizacao = dto.DataDeAtualizacao,
            UserId = dto.UserId,
            RegistroDaConta = null!,
            Arquivo = null!,
            User = null!
        };
    }    
    public static ArquivoDoRegistroDaContaDto ToDto(this ArquivoDoRegistroDaConta entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        return new ArquivoDoRegistroDaContaDto
        {
            Id = entity.Id,
            RegistroDaContaId = entity.RegistroDaContaId,
            ArquivoId = entity.ArquivoId,
            ModalidadeDoArquivo = entity.ModalidadeDoArquivo,
            DataDeCriacao = entity.DataDeCriacao,
            DataDeAtualizacao = entity.DataDeAtualizacao,
            UserId = entity.UserId,
            RegistroDaConta = null, // Evita referência circular
            Arquivo = entity.Arquivo?.ToDto(),
            //User = entity.User?.ToDto()
        };
    }

    public static void FromDto(this ArquivoDoRegistroDaConta entity, ArquivoDoRegistroDaContaDto dto)
    {
        ArgumentNullException.ThrowIfNull(entity);
        ArgumentNullException.ThrowIfNull(dto);

        entity.Id = dto.Id;
        entity.RegistroDaContaId = dto.RegistroDaContaId;
        entity.ArquivoId = dto.ArquivoId;
        entity.ModalidadeDoArquivo = dto.ModalidadeDoArquivo;
        entity.DataDeAtualizacao = dto.DataDeAtualizacao;
        entity.UserId = dto.UserId;
    }    
}
