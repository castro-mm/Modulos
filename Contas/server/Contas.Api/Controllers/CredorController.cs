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
    public async Task<ActionResult> GetAsync([FromQuery] CredorParams credorParams, CancellationToken cancellationToken)
    {        
        var spec = new CredorSpecification(credorParams);
        var lista = await service.GetAsyncWithSpec(spec, cancellationToken);

        if (lista == null || !lista.Any())
            return NotFound("Nenhum registro encontrado com os parâmetros informados.");
        
        return Ok(lista);
    }
}
