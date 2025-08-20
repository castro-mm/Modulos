using Contas.Core.Dtos;
using Contas.Core.Entities.Base;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;

namespace Contas.Core.Entities;

public class Credor : Entity, IConvertibleToDto<CredorDto>
{
    public int SegmentoDoCredorId { get; set; }
    public required string RazaoSocial { get; set; }
    public required string NomeFantasia { get; set; }
    public long CNPJ { get; set; }

    public virtual ICollection<RegistroDaConta>? RegistrosDaConta { get; set; }   
    public required virtual SegmentoDoCredor SegmentoDoCredor { get; set; }

    public void ConvertFromDto(CredorDto dto) => this.FromDto(dto);
    public CredorDto ConvertToDto() => this.ToDto();
}
