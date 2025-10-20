using Contas.Api.Objects;
using Contas.Core.Entities.Base;
using Contas.Core.Helpers;
using Contas.Core.Interfaces;
using Contas.Core.Specifications.Base;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public abstract class BaseApiController<TDto, TEntity>(IService<TDto, TEntity> service) : ControllerBase
    where TDto : IDto
    where TEntity : Entity
{
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult> GetAllAsync([FromQuery] SpecificationParams specParams, CancellationToken cancellationToken)
    {
        var specs = FactoryHelper.CreateInstance<Specification<TEntity>>(specParams);

        var pagedResult = await service.GetPagedResultWithSpecAsync(specs, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult == null || !pagedResult.Items.Any())
            return NotFound("Nenhum registro encontrado.");

        return Ok(pagedResult);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return BadRequest("O ID deve ser maior que zero.");

        if (!await service.ExistsAsync(id, cancellationToken))
            return NotFound("O item informado não existe.");

        var dto = await service.GetByIdAsync(id, cancellationToken);
        if (dto == null)
            return BadRequest("Erro ao buscar o item. Entre em contato com o administrador do sistema.");

        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        if (dto == null)
            return BadRequest("Os dados não foram informados.");

        var result = await service.CreateAsync(dto, cancellationToken);
        if (result == null)
            return BadRequest("Erro ao adicionar as informações enviadas. Entre em contato com o administrador do sistema.");

        return Ok(result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult> UpdateAsync(int id, [FromBody] TDto dto, CancellationToken cancellationToken)
    {
        if (dto == null)
            return BadRequest("Os dados não foram informados.");

        if (dto.Id != id)
            return BadRequest("O ID do registro não corresponde ao ID da URL.");

        if (!await service.ExistsAsync(id, cancellationToken))
            return NotFound("O registro com o ID informado não existe.");

        var result = await service.UpdateAsync(dto, cancellationToken);
        if (result == null)
            return BadRequest("Erro ao atualizar as informações. Entre em contato com o administrador do sistema.");

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public virtual async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return BadRequest("O ID informado deve ser maior que zero.");

        if (!await service.ExistsAsync(id, cancellationToken))
            return NotFound("O registro com o ID informado não existe."); 

        var result = await service.DeleteAsync(id, cancellationToken);
        if (result)
            return Ok("Registro excluído com sucesso.");

        return BadRequest("Erro ao excluir o registro. Entre em contato com o administrador do sistema.");
    }

    [HttpDelete("delete-range")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public virtual async Task<ActionResult> DeleteRangeAsync([FromBody] IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        if (ids == null || !ids.Any() || ids.Any(id => id <= 0))
            return BadRequest("Os IDs informados são inválidos.");

        var result = await service.DeleteRangeAsync(ids, cancellationToken);
        if (result)
            return Ok($"Os {ids.Count()} registro(s) foi(ram) excluído(s) com sucesso.");

        return BadRequest("Erro ao excluir os registros. Entre em contato com o administrador do sistema.");
    }
}

