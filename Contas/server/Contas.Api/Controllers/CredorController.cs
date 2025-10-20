using Contas.Api.Controllers.Base;
using Contas.Api.Objects;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

public class CredorController : BaseApiController<CredorDto, Credor>
{    
    private readonly ICredorService _service;

    public CredorController(ICredorService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-params")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]    
    public async Task<ActionResult> GetAsync([FromQuery]CredorParams specParams, CancellationToken cancellationToken)
    {
        if (specParams == null)
            return BadRequest("Os parâmetros de consulta não foram informados.");

        var spec = new CredorSpecification(specParams);
        var pagedResult = await _service.GetPagedResultWithSpecAsync(spec, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult == null || !pagedResult.Items.Any())
            return NotFound("Nenhum registro encontrado com os parâmetros informados.");

        return Ok(pagedResult);
    }
}
