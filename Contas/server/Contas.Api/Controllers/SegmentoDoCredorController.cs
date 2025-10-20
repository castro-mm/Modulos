using Contas.Api.Controllers.Base;
using Contas.Api.Objects;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

public class SegmentoDoCredorController : BaseApiController<SegmentoDoCredorDto, SegmentoDoCredor>
{    
    private readonly ISegmentoDoCredorService _service;

    public SegmentoDoCredorController(ISegmentoDoCredorService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-params")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetByParamsAsync([FromQuery]SegmentoDoCredorParams specParams, CancellationToken cancellationToken)
    {
        if (specParams == null)
            return BadRequest("Os parâmetros de consulta não foram informados.");

        var spec = new SegmentoDoCredorSpecification(specParams);
        var pagedResult = await _service.GetPagedResultWithSpecAsync(spec, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult.Items == null || pagedResult.Count == 0)
            return NotFound("Nenhum registro encontrado com os parâmetros informados.");

        return Ok(pagedResult);
    }
}
