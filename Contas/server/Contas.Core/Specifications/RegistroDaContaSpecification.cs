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

        // Combinar todos os critérios em uma única expressão
        AddCriteria(x => 
            (x.Mes == specParams.Mes || specParams.Mes == 0) &&
            (x.Ano == specParams.Ano || specParams.Ano == 0) &&
            (x.PagadorId == specParams.PagadorId || specParams.PagadorId == null) &&
            (x.CredorId == specParams.CredorId || specParams.CredorId == null) &&
            (
                x.DataDePagamento.HasValue && (specParams.StatusDaConta == StatusDaConta.Paga) ||
                !x.DataDePagamento.HasValue && (
                    x.DataDeVencimento < DateTime.Now && (specParams.StatusDaConta == StatusDaConta.Vencida) ||    
                    x.DataDeVencimento >= DateTime.Now && (specParams.StatusDaConta == StatusDaConta.Pendente) 
                ) ||               
                (specParams.StatusDaConta == StatusDaConta.Todos || specParams.StatusDaConta == null)
            )
        );
    }
}
