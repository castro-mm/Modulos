using Contas.Core.Specifications.Base;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Core.Specifications.Params;

public class RegistroDaContaSpecParams : SpecificationParams
{
    public int Mes { get; set; }
    public int Ano { get; set; }
    public int? PagadorId { get; set; }
    public int? CredorId { get; set; }
    public StatusDaConta? StatusDaConta { get; set; }
}