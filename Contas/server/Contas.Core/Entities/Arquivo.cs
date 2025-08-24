using Contas.Core.Dtos;
using Contas.Core.Entities.Base;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Core.Entities;

public class Arquivo : Entity, IConvertibleToDto<ArquivoDto>
{
    public int RegistroDaContaId { get; set; }
    public required string Nome { get; set; }
    public required string Extensao { get; set; }
    public long Tamanho { get; set; }
    public required byte[] Conteudo { get; set; }
    public TipoDeArquivo Tipo { get; set; }

    public required virtual RegistroDaConta RegistroDaConta { get; set; }

    public ArquivoDto ConvertToDto() => this.ToDto();
    public void ConvertFromDto(ArquivoDto dto) => this.FromDto(dto);
}
