using Contas.Core.Dtos;
using Contas.Core.Entities.Base;
using Contas.Core.Interfaces;
using Contas.Core.Mappings;

namespace Contas.Core.Entities;

public class SegmentoDoCredor : Entity, IConvertibleToDto<SegmentoDoCredorDto>
{
    public required string Nome { get; set; }
    public virtual ICollection<Credor> Credores { get; set; } = [];

    public SegmentoDoCredorDto ConvertToDto() => this.ToDto(); 
    public void ConvertFromDto(SegmentoDoCredorDto dto) => this.FromDto(dto);    
}