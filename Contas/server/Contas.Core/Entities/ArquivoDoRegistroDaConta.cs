using Contas.Core.Dtos;
using Contas.Core.Entities.Base;
using Contas.Core.Entities.System;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Core.Entities;

public class ArquivoDoRegistroDaConta : Entity, IConvertibleToDto<ArquivoDoRegistroDaContaDto>
{
    public required int RegistroDaContaId { get; set; }
    public required int ArquivoId { get; set; }
    public required ModalidadeDoArquivo ModalidadeDoArquivo { get; set; }

    public virtual RegistroDaConta RegistroDaConta { get; set; } = null!;
    public virtual Arquivo Arquivo { get; set; } = null!;

    public void ConvertFromDto(ArquivoDoRegistroDaContaDto dto) => this.FromDto(dto);
    public ArquivoDoRegistroDaContaDto ConvertToDto() => this.ToDto();
}

public static class ArquivoDoRegistroDaContaExtensions
{
    
}