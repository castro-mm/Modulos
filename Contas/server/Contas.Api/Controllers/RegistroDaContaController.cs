using Contas.Api.Controllers.Base;
using Contas.Api.Objects;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

public class RegistroDaContaController : BaseApiController<RegistroDaContaDto, RegistroDaConta>
{
    private readonly IRegistroDaContaService _service;

    public RegistroDaContaController(IRegistroDaContaService service) : base(service)
    {
        _service = service;
    }

    [HttpGet("get-by-params")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult> GetAsync([FromQuery]RegistroDaContaSpecParams specParams, CancellationToken cancellationToken)
    {
        if (specParams == null)
            return BadRequest("Os parâmetros de consulta não foram informados.");

        var spec = new RegistroDaContaSpecification(specParams);
        var pagedResult = await _service.GetPagedResultWithSpecAsync(spec, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult.Items == null || pagedResult.Count == 0)
            return NotFound("Nenhum registro encontrado com os parâmetros informados.");

        return Ok(pagedResult);
    }
}
