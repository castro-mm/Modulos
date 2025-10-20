using Contas.Core.Entities;
using Contas.Core.Specifications.Base;
using Contas.Core.Specifications.Params;

namespace Contas.Core.Specifications;

public class PagadorSpecification : Specification<Pagador>
{
    public PagadorSpecification(PagadorParams specParams) : base(specParams)
    {
        AddCriteria(x => x.Nome.Contains(specParams.Nome ?? string.Empty) || string.IsNullOrEmpty(specParams.Nome));
        AddCriteria(x => x.Email.Contains(specParams.Email ?? string.Empty) || string.IsNullOrEmpty(specParams.Email));
        AddCriteria(x => x.CPF == specParams.CPF || specParams.CPF == null);
    }        
}