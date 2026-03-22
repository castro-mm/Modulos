using Contas.Api.Controllers.Base;
using Contas.Api.Objects;
using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Interfaces.Services;
using Contas.Core.Interfaces.Services.Security;
using Contas.Core.Objects;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

public class CredorController : BaseApiController<CredorDto, Credor>
{    
    private readonly ICredorService _service;
    private readonly ICredorValidator _validator;
    private readonly ICurrentUserService _currentUserService;

    public CredorController(ICredorService service, ICredorValidator validator, ICurrentUserService currentUserService) : base(service, validator)
    {
        _service = service;
        _validator = validator;
        _currentUserService = currentUserService;
    }

    [HttpGet("get-by-params")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]    
    public async Task<ActionResult> GetAsync([FromQuery]CredorParams specParams, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResult();

        if (specParams == null)
        {
            validationResult.AddError("PARAMETROS_NAO_INFORMADOS", "Os parâmetros de consulta não foram informados.");
            return BadRequest(Result.Failure(validationResult.Errors));
        }

        var spec = new CredorSpecification(specParams, _currentUserService);
        var pagedResult = await _service.GetPagedResultWithSpecAsync(spec, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult == null || !pagedResult.Items.Any())
        {
            validationResult.AddError("REGISTROS_NAO_ENCONTRADOS", "Nenhum registro encontrado com os parâmetros informados.");
            return NotFound(Result.Failure(validationResult.Errors));
        }
        
        return Ok(Result.Successful(pagedResult));
    }
}
