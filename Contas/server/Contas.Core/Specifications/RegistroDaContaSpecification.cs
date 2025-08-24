using Contas.Core.Entities;
using Contas.Core.Specifications.Base;
using Contas.Core.Specifications.Params;
using static Contas.Core.Objects.Enumerations;

namespace Contas.Core.Specifications;

public class RegistroDaContaSpecification : Specification<RegistroDaConta>
{
    public RegistroDaContaSpecification()
    {
        AddInclude(x => x.Credor);
        AddInclude(x => x.Pagador);
        AddInclude(x => x.Arquivos);
    }

    public RegistroDaContaSpecification(RegistroDaContaSpecParams specParams) : this()
    {
        AddCriteria(x =>
            (x.PagadorId == specParams.PagadorId || specParams.PagadorId == null || specParams.PagadorId == 0)
            && (x.CredorId == specParams.CredorId || specParams.CredorId == null || specParams.CredorId == 0)
            && (x.Mes == specParams.Mes || specParams.Mes == null || specParams.Mes == 0)
            && (x.Ano == specParams.Ano || specParams.Ano == null || specParams.Ano == 0)
            && (x.Status == (StatusDaConta)specParams.StatusDaConta || specParams.StatusDaConta == -1)
        );
    }
}
