using Contas.Core.Entities;
using Contas.Core.Specifications.Base;
using Contas.Core.Specifications.Params;

namespace Contas.Core.Specifications;

public class CredorSpecification : Specification<Credor>
{
    public CredorSpecification(CredorParams specParams) : base(specParams)
    {
        AddInclude(x => x.SegmentoDoCredor);

        AddCriteria(x => x.SegmentoDoCredorId == specParams.SegmentoDoCredorId || specParams.SegmentoDoCredorId == null || specParams.SegmentoDoCredorId == 0);
        AddCriteria(x => x.NomeFantasia.Contains(specParams.NomeFantasia ?? string.Empty) || string.IsNullOrEmpty(specParams.NomeFantasia));
        AddCriteria(x => x.RazaoSocial.Contains(specParams.RazaoSocial ?? string.Empty) || string.IsNullOrEmpty(specParams.RazaoSocial)); 
        AddCriteria(x => x.CNPJ == specParams.CNPJ || specParams.CNPJ == null || specParams.CNPJ == 0);        
    }
}
