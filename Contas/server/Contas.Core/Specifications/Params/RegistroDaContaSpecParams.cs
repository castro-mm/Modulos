using Contas.Core.Specifications.Base;

namespace Contas.Core.Specifications.Params;

public class RegistroDaContaSpecParams : SpecificationParams
{
    public int? Mes { get; set; }
    public int? Ano { get; set; }
    public int? PagadorId { get; set; }
    public int? CredorId { get; set; }
    public int StatusDaConta { get; set; } = 99;
}