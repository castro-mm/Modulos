using Contas.Core.Dtos.System;
using Contas.Core.Entities.Base;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;

namespace Contas.Core.Entities.System;

public class Arquivo : Entity, IConvertibleToDto<ArquivoDto>
{
    public required string Nome { get; set; }
    public required string Extensao { get; set; }
    public required long Tamanho { get; set; }
    public required string Tipo { get; set; }
    public required byte[] Dados { get; set; } 
    public required DateTime DataDaUltimaModificacao { get; set; }

    public virtual ArquivoDoRegistroDaConta? ArquivoDoRegistroDaConta { get; set; }

    public void ConvertFromDto(ArquivoDto dto) => this.FromDto(dto);
    public ArquivoDto ConvertToDto() => this.ToDto();
}

public static class ArquivoExtensions
{
}
