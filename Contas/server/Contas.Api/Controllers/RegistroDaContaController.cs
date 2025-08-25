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
    [HttpGet("get-by")]
    public virtual async Task<ActionResult> GetAsync([FromQuery] RegistroDaContaSpecParams specParams, CancellationToken cancellationToken)
    {
        if (specParams == null)
            return BadRequest("Os parâmetros de consulta não foram informados.");

        var spec = new RegistroDaContaSpecification(specParams);

        var pagedResult = await service.GetPagedResultWithSpecAsync(spec, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult.Itens == null || pagedResult.Count == 0)
            return NotFound("Nenhum registro encontrado com os parâmetros informados.");

        return Ok(pagedResult);
    }
}
