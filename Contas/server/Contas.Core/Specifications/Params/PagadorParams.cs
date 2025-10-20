using Contas.Core.Specifications.Base;

namespace Contas.Core.Specifications.Params;

public class PagadorParams : SpecificationParams
{
    public string? Nome { get; set; }
    public string? Email { get; set; }
    public long? CPF { get; set; }
}
