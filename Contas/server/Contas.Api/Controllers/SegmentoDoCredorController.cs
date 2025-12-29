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

public class SegmentoDoCredorController : BaseApiController<SegmentoDoCredorDto, SegmentoDoCredor>
{    
    private readonly ISegmentoDoCredorService _service;
    private readonly ISegmentoDoCredorValidator _validator;
    
    public SegmentoDoCredorController(ISegmentoDoCredorService service, ISegmentoDoCredorValidator validator) : base(service, validator)
    {
        _service = service;
        _validator = validator;
    }

    [HttpGet("get-by-params")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetByParamsAsync([FromQuery]SegmentoDoCredorParams specParams, CancellationToken cancellationToken)
    {
        // generalizar o validationResult para um método reutilizável
        var validationResult = _validator.Validate(specParams);
        if (!validationResult.IsValid)
            return BadRequest(Result.Failure(validationResult.Errors));

        var spec = new SegmentoDoCredorSpecification(specParams);
        var pagedResult = await _service.GetPagedResultWithSpecAsync(spec, specParams.PageIndex, specParams.PageSize, cancellationToken);

        validationResult = _validator.Validate(pagedResult);
        if (!validationResult.IsValid)
            return NotFound(Result.Failure(validationResult.Errors));

        return Ok(Result.Successful(pagedResult));
    }      
}