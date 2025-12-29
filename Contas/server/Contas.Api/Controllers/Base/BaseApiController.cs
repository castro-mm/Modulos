using Contas.Api.Objects;
using Contas.Core.Businesses.Validators.Interfaces;
using Contas.Core.Entities.Base;
using Contas.Core.Helpers;
using Contas.Core.Interfaces;
using Contas.Core.Objects;
using Contas.Core.Specifications.Base;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseApiController<TDto, TEntity> : ControllerBase where TDto : IDto where TEntity : Entity
{
    private readonly IService<TDto, TEntity> _service;
    private readonly IValidator<TDto> _validator;

    public BaseApiController(IService<TDto, TEntity> service, IValidator<TDto> validator)
    {
        _service = service;
        _validator = validator;
    }

    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult> GetAllAsync([FromQuery] SpecificationParams specParams, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResult();

        var specs = FactoryHelper.CreateInstance<Specification<TEntity>>(specParams);

        var pagedResult = await _service.GetPagedResultWithSpecAsync(specs, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult == null || !pagedResult.Items.Any())
            validationResult.AddError("REGISTROS_NAO_ENCONTRADOS", "Nenhum registro encontrado.");

        return validationResult.IsValid
            ? Ok(Result.Successful(pagedResult))
            : NotFound(Result.Failure(validationResult.Errors));
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResult();

        if (id <= 0)
            validationResult.AddError("ID_INVALIDO", "O ID deve ser maior que zero.");
        else if (!await _service.ExistsAsync(id, cancellationToken))
            validationResult.AddError("REGISTRO_NAO_ENCONTRADO", "O item informado não existe.");
        
        if (!validationResult.IsValid)
            return BadRequest(Result.Failure(validationResult.Errors));
        
        var dto = await _service.GetByIdAsync(id, cancellationToken);
        if (dto == null)
            validationResult.AddError("ERRO_INTERNO", "Erro ao buscar o item. Entre em contato com o administrador do sistema.");

        return validationResult.IsValid
            ? Ok(Result.Successful(dto))
            : BadRequest(Result.Failure(validationResult.Errors));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(dto);
        
        if (!validationResult.IsValid)
            return BadRequest(Result.Failure(validationResult.Errors));

        var result = await _service.CreateAsync(dto, cancellationToken);
        if (result == null)
            validationResult.AddError("ERRO_INTERNO", "Erro ao adicionar as informações enviadas. Entre em contato com o administrador do sistema.");

        return validationResult.IsValid
            ? Ok(Result.Successful(result, "Registro criado com sucesso."))
            : BadRequest(Result.Failure(validationResult.Errors));    
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult> UpdateAsync(int id, [FromBody] TDto dto, CancellationToken cancellationToken)
    {
        var validationResult = _validator.Validate(dto);

        if (dto.Id != id)
            validationResult.AddError("ID_INCONSISTENTE", "O ID do registro não corresponde ao ID da URL.");
        else if (id == 0)
            validationResult.AddError("ID_INVALIDO", "O ID informado deve ser maior que zero.");
        else if (!await _service.ExistsAsync(id, cancellationToken))
            validationResult.AddError("REGISTRO_NAO_ENCONTRADO", "O registro com o ID informado não existe.");

        if (!validationResult.IsValid)
            return BadRequest(Result.Failure(validationResult.Errors));

        var result = await _service.UpdateAsync(dto, cancellationToken);
        if (result == null)
            validationResult.AddError("ERRO_INTERNO", "Erro ao atualizar as informações enviadas. Entre em contato com o administrador do sistema.");

        return validationResult.IsValid
            ? Ok(Result.Successful(result, "Registro atualizado com sucesso."))
            : BadRequest(Result.Failure(validationResult.Errors));
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResult();

        if (id <= 0)
            validationResult.AddError("ID_INVALIDO", "O ID informado deve ser maior que zero.");
        else if (!await _service.ExistsAsync(id, cancellationToken))
            validationResult.AddError("REGISTRO_NAO_ENCONTRADO", "O registro com o ID informado não existe.");
        else
        {
            var result = await _service.DeleteAsync(id, cancellationToken);
            if (!result)
                validationResult.AddError("ERRO_INTERNO", "Erro ao excluir as informações enviadas. Entre em contato com o administrador do sistema.");            
        }

        return validationResult.IsValid
            ? Ok(Result.Successful<string>(message: "Registro excluído com sucesso."))
            : BadRequest(Result.Failure(validationResult.Errors));
    }

    [HttpDelete("delete-range")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult> DeleteRangeAsync([FromBody] IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        var validationResult = new ValidationResult();

        if (ids == null || !ids.Any() || ids.Any(id => id <= 0)) {
            validationResult.AddError("IDS_INVALIDOS", "Os IDs informados são inválidos.");
        }
        else {
            var result = await _service.DeleteRangeAsync(ids, cancellationToken);
            if (!result)
                validationResult.AddError("ERRO_INTERNO", "Erro ao excluir as informações enviadas. Entre em contato com o administrador do sistema.");            
        }
    
        return validationResult.IsValid
            ? Ok(Result.Successful<string>(message: $"Os {ids!.Count()} registro(s) foi(ram) excluído(s) com sucesso."))
            : BadRequest(Result.Failure(validationResult.Errors));
    }
}

