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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult> GetAllAsync([FromQuery]SpecificationParams specParams, CancellationToken cancellationToken)
    {              
        var specs = FactoryHelper.CreateInstance<Specification<TEntity>>(specParams);

        var pagedResult = await service.GetPagedResultWithSpecAsync(specs, specParams.PageIndex, specParams.PageSize, cancellationToken);

        if (pagedResult == null || !pagedResult.Itens.Any())
            return NotFound("Nenhum registro encontrado.");

        return Ok(pagedResult);
    }

    [HttpGet("{id:int}"/*, Name = nameof(GetByIdAsync)*/)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return BadRequest("O ID deve ser maior que zero.");

        if (!await service.ExistsAsync(id, cancellationToken))
            return NotFound("O item informado não existe.");

        var dto = await service.GetByIdAsync(id, cancellationToken);
        if (dto == null)
            return BadRequest("Erro ao buscar o item.");

        return Ok(dto);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        if (dto == null)
            return BadRequest("Os dados não foram informados.");

        var result = await service.CreateAsync(dto, cancellationToken);
        if (result == null)
            return BadRequest("Erro ao adicionar as informações enviadas.");

        return Ok(result);

        //return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
            return BadRequest("Erro ao atualizar as informações.");

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return BadRequest("O ID informado deve ser maior que zero.");

        if (!await service.ExistsAsync(id, cancellationToken))
            return NotFound("O registro com o ID informado não existe.");

        var result = await service.DeleteAsync(id, cancellationToken);
        if (result)
            return NoContent();

        return BadRequest("Erro ao excluir o registro.");
    }    
}

