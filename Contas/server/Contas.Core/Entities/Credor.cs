using Contas.Core.Dtos;
using Contas.Core.Entities.Base;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;

namespace Contas.Core.Entities;

public class Credor : Entity, IConvertibleToDto<CredorDto>
{
    public required int SegmentoDoCredorId { get; set; }
    public required string RazaoSocial { get; set; }
    public required string NomeFantasia { get; set; }
    public long CNPJ { get; set; }

    public virtual ICollection<RegistroDaConta>? RegistrosDaConta { get; set; }   
    public virtual SegmentoDoCredor SegmentoDoCredor { get; set; } = null!;

    public void ConvertFromDto(CredorDto dto) => this.FromDto(dto);
    public CredorDto ConvertToDto() => this.ToDto();
}
