using Contas.Core.Entities;
using Contas.Core.Extensions;
using Contas.Core.Specifications.Base;
using Contas.Core.Specifications.Params;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Core.Specifications;

public class RegistroDaContaSpecification : Specification<RegistroDaConta>
{
    public RegistroDaContaSpecification(RegistroDaContaSpecParams specParams) : base(specParams)
    {
        AddInclude(x => x.Credor);
        AddInclude(x => x.Pagador);
        AddInclude(x => x.Arquivos);

        var statusDaConta = specParams.StatusDaConta.ToEnum<StatusDaConta>();

        AddCriteria(x => x.PagadorId == specParams.PagadorId || specParams.PagadorId == null || specParams.PagadorId == 0);
        AddCriteria(x => x.CredorId == specParams.CredorId || specParams.CredorId == null || specParams.CredorId == 0);
        AddCriteria(x => x.Mes == specParams.Mes || specParams.Mes == null || specParams.Mes == 0);
        AddCriteria(x => x.Ano == specParams.Ano || specParams.Ano == null || specParams.Ano == 0);
        AddCriteria(x => x.Status == statusDaConta || statusDaConta == StatusDaConta.Todos);
    }
}
