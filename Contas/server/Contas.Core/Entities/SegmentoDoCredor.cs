using Contas.Core.Dtos;
using Contas.Core.Entities.Base;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;

namespace Contas.Core.Entities;

public class SegmentoDoCredor : Entity, IConvertibleToDto<SegmentoDoCredorDto>
{
    public required string Nome { get; set; }
    public ICollection<Credor>? Credores { get; set; }

    // pensar em trazer a conversão para a própria classe SegmentoDoCredor ou na Entity (ele fica chamando ele mesmo)
    public SegmentoDoCredorDto ConvertToDto() => this.ToDto(); 
    public void ConvertFromDto(SegmentoDoCredorDto dto) => this.UpdateFromDto(dto);    
}