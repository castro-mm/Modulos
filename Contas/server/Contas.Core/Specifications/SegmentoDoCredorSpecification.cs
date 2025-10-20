using Contas.Core.Entities;
using Contas.Core.Specifications.Base;
using Contas.Core.Specifications.Params;

namespace Contas.Core.Specifications;

public class SegmentoDoCredorSpecification : Specification<SegmentoDoCredor>
{
    public SegmentoDoCredorSpecification(SegmentoDoCredorParams specParams) : base(specParams)
    {
        AddInclude(x => x.Credores);

        AddCriteria(x => x.Nome.Contains(specParams.Nome ?? string.Empty) || string.IsNullOrEmpty(specParams.Nome));
    }
}