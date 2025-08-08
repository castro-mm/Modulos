using Contas.Core.Entities.Base;

namespace Contas.Core.Entities;

public class Pagador : Entity
{
    public required string Nome { get; set; }
    public required string Email { get; set; }
    public long CPF { get; set; }
    public ICollection<RegistroDaConta>? RegistrosDaConta { get; set; }
}
