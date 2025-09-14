using Contas.Core.Specifications.Base;

namespace Contas.Core.Specifications.Params;

public class SegmentoDoCredorParams : SpecificationParams
{
    public string? Nome { get; set; } = string.Empty;
}
