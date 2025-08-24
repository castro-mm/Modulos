using Contas.Api.Controllers.Base;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

public class RegistroDaContaController(IRegistroDaContaService service) : BaseApiController<RegistroDaContaDto, RegistroDaConta>(service)
{
    // This controller can be extended with specific functionality for RegistroDaConta
    // For example, you might want to add methods for specific queries or operations related to RegistroDaConta.
    // The base functionality is already provided by BaseApiController.

    // TODO: Avaliar a criação deste metodo na base BaseApiController criando uma instancia como e feita no repositorio
    //       para receber a SpecificationParams e criar a Specification generica
    //       Assim todos os controllers que precisarem de filtros por parametros podem usar este metodo
    //       Exemplo: public virtual async Task<ActionResult> Get([FromQuery] SpecificationParams specParams, CancellationToken cancellationToken)
    [HttpGet("get-by")]
    public virtual async Task<ActionResult> GetAsync([FromQuery] RegistroDaContaSpecParams specParams, CancellationToken cancellationToken)
    {
        if (specParams == null)
            return BadRequest("Os parâmetros de consulta não foram informados.");

        var spec = new RegistroDaContaSpecification(specParams);

        var lista = await service.GetAsyncWithSpec(spec, cancellationToken);

        if (lista == null || !lista.Any())
            return NotFound("Nenhum registro encontrado com os parâmetros informados.");

        return Ok(lista);
    }
}
