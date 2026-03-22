using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Interfaces;
using Contas.Core.Interfaces.Services.Security;
using Contas.Core.Specifications.Base;
using Contas.Core.Specifications.Params;

namespace Contas.Core.Specifications;

public class SegmentoDoCredorSpecification : Specification<SegmentoDoCredor>
{
    public SegmentoDoCredorSpecification(SegmentoDoCredorParams specParams, ICurrentUserService currentUserService) : base(specParams)
    {
        AddInclude(x => x.Credores);

        AddCriteria(x => 
            x.Nome.Contains(specParams.Nome ?? string.Empty) || string.IsNullOrEmpty(specParams.Nome) &&
            (x.UserId == currentUserService.UserId));
    }
}

