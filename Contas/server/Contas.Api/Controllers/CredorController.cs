using Contas.Api.Controllers.Base;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

public class CredorController(ICredorService service) : BaseApiController<CredorDto, Credor>(service)
{
    [HttpGet("get-by")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAsync([FromQuery] CredorParams specParams, CancellationToken cancellationToken)
    {        
        if (specParams == null)
            return BadRequest("Os parâmetros de consulta não foram informados.");

        var spec = new CredorSpecification(specParams);
        var pagedResult = await service.GetPagedResultWithSpecAsync(spec, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult == null || !pagedResult.Itens.Any())
            return NotFound("Nenhum registro encontrado com os parâmetros informados.");
        
        return Ok(pagedResult);
    }
}
