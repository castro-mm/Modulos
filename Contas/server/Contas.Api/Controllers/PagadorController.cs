using Contas.Api.Controllers.Base;
using Contas.Api.Objects;
using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Objects;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

public class PagadorController : BaseApiController<PagadorDto, Pagador>
{    
    private readonly IPagadorService _service;
    private readonly IPagadorValidator _validator;

    public PagadorController(IPagadorService service, IPagadorValidator validator) : base(service, validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpGet("get-by-params")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)] 
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetAsync([FromQuery]PagadorParams specParams, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResult();

        if (specParams == null)
        {
            validationResult.AddError("PARAMETROS_NAO_INFORMADOS", "Os parâmetros de consulta não foram informados.");
            return BadRequest(Result.Failure(validationResult.Errors));
        }

        var spec = new PagadorSpecification(specParams);
        var pagedResult = await _service.GetPagedResultWithSpecAsync(spec, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult == null || !pagedResult.Items.Any())
        {
            validationResult.AddError("REGISTROS_NAO_ENCONTRADOS", "Nenhum registro encontrado com os parâmetros informados."); 
            return NotFound(Result.Failure(validationResult.Errors));
        }

        return Ok(Result.Successful(pagedResult));
    }
}