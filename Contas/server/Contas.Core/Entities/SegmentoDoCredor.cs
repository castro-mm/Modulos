using Contas.Core.Entities.Base;

namespace Contas.Core.Entities;

public class SegmentoDoCredor : Entity
{
    public required string Nome { get; set; }
    public ICollection<Credor>? Credores { get; set; }
}