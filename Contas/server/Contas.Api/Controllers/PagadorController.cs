using Contas.Api.Controllers.Base;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

public class PagadorController(IPagadorService service) : BaseApiController<PagadorDto, Pagador>(service)
{
    [HttpGet("get-by")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]    
    public async Task<ActionResult> GetAsync([FromQuery] PagadorParams specParams, CancellationToken cancellationToken)
    {
        if (specParams == null)
            return BadRequest("Os parâmetros de consulta não foram informados.");

        var spec = new PagadorSpecification(specParams);
        var lista = await service.GetAsyncWithSpec(spec, cancellationToken);

        if (lista == null || !lista.Any())
            return NotFound("Nenhum pagador encontrado com o nome informado");

        return Ok(lista);
    }
}
