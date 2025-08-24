using Contas.Core.Entities;
using Contas.Core.Specifications.Base;
using Contas.Core.Specifications.Params;

namespace Contas.Core.Specifications;

public class SegmentoDoCredorSpecification : Specification<SegmentoDoCredor>
{
    public SegmentoDoCredorSpecification() => AddInclude(x => x.Credores);
    public SegmentoDoCredorSpecification(SegmentoDoCredorParams specParams) : this()
        => AddCriteria(x =>
            x.Nome.Contains(specParams.Nome ?? string.Empty) || string.IsNullOrEmpty(specParams.Nome)
            );    
}
