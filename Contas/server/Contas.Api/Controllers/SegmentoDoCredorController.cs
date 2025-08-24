using Contas.Api.Controllers.Base;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

public class SegmentoDoCredorController(ISegmentoDoCredorService service) : BaseApiController<SegmentoDoCredorDto, SegmentoDoCredor>(service)
{
    // This controller can be extended with specific functionality for SegmentoDoCredor
    // For example, you might want to add methods for specific queries or operations related to SegmentoDoCredor.
    // The base functionality is already provided by BaseApiController.

    [HttpGet("get-by")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAsync([FromQuery]SegmentoDoCredorParams specParams, CancellationToken cancellationToken)
    {
        if (specParams == null)
            return BadRequest("Os parâmetros de consulta não foram informados.");

        var spec = new SegmentoDoCredorSpecification(specParams);
        var lista = await service.GetAsyncWithSpec(spec, cancellationToken);

        if (lista == null || !lista.Any())
            return NotFound("Nenhum registro encontrado com os parâmetros informados.");

        return Ok(lista);
    }
}
