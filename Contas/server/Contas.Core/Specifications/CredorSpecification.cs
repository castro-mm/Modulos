using Contas.Core.Entities;
using Contas.Core.Specifications.Base;
using Contas.Core.Specifications.Params;

namespace Contas.Core.Specifications;

public class CredorSpecification : Specification<Credor>
{
    public CredorSpecification(CredorParams specParams) : base(specParams)
    {
        AddInclude(x => x.SegmentoDoCredor);

        AddCriteria(x =>
            (x.SegmentoDoCredorId == specParams.SegmentoDoCredorId || specParams.SegmentoDoCredorId == null || specParams.SegmentoDoCredorId == 0) &&
            (x.NomeFantasia.Contains(specParams.NomeFantasia ?? string.Empty) || string.IsNullOrEmpty(specParams.NomeFantasia)) &&
            (x.RazaoSocial.Contains(specParams.RazaoSocial ?? string.Empty) || string.IsNullOrEmpty(specParams.RazaoSocial))
        );
    }
}
