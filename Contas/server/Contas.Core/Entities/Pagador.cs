using Contas.Core.Dtos;
using Contas.Core.Entities.Base;
using Contas.Core.Extensions;
using Contas.Core.Interfaces;

namespace Contas.Core.Entities;

public class Pagador : Entity, IConvertibleToDto<PagadorDto>
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public long CPF { get; set; }

    public virtual ICollection<RegistroDaConta>? RegistrosDaConta { get; set; }

    public void ConvertFromDto(PagadorDto dto) => this.FromDto(dto);
    public PagadorDto ConvertToDto() => this.ToDto();
}
