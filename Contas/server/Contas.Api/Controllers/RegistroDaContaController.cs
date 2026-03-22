using Contas.Api.Controllers.Base;
using Contas.Api.Objects;
using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Core.Objects;
using Contas.Core.Specifications;
using Contas.Core.Specifications.Params;
using Microsoft.AspNetCore.Mvc;
using Contas.Core.Interfaces.Services;
using Contas.Core.Interfaces.Services.Security;

namespace Contas.Api.Controllers;

public class RegistroDaContaController : BaseApiController<RegistroDaContaDto, RegistroDaConta>
{
    private readonly IRegistroDaContaService _service;
    private readonly IRegistroDaContaValidator _validator;
    private readonly ICurrentUserService _currentUserService;

    public RegistroDaContaController(IRegistroDaContaService service, IRegistroDaContaValidator validator, ICurrentUserService currentUserService) : base(service, validator)
    {
        _service = service;
        _validator = validator;
        _currentUserService = currentUserService;
    }

    [HttpGet("get-by-params")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult> GetAsync([FromQuery]RegistroDaContaSpecParams specParams, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResult();
        
        if (specParams == null)
        {
            validationResult.AddError("PARAMETROS_NAO_INFORMADOS", "Os parâmetros de consulta não foram informados.");
            return BadRequest(Result.Failure(validationResult.Errors));
        }

        var spec = new RegistroDaContaSpecification(specParams, _currentUserService);
        var pagedResult = await _service.GetPagedResultWithSpecAsync(spec, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult.Items == null || pagedResult.Count == 0)
        {
            validationResult.AddError("REGISTROS_NAO_ENCONTRADOS", "Nenhum registro encontrado com os parâmetros informados.");
            return NotFound(Result.Failure(validationResult.Errors));
        }
        return Ok(Result.Successful(pagedResult));
    }   
}
