using Contas.Core.Specifications.Base;

namespace Contas.Core.Specifications.Params;

public class CredorParams : SpecificationParams
{
    public int? SegmentoDoCredorId { get; set; }
    public string? Nome { get; set; }
}
