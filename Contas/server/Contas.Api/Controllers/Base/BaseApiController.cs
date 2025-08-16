using Contas.Core.Interfaces;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController<TDto>(IService<TDto> service) : ControllerBase where TDto : IDto
{
    // This class can be extended with common functionality for all API controllers
    // For example, logging, error handling, or shared services can be injected here.
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<IEnumerable<TDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var lista = await service.GetAllAsync(cancellationToken);

        if (lista == null || !lista.Any())
            return NotFound("Nenhum item encontrado.");

        return Ok(lista);
    }

    [HttpGet("{id:int}", Name = nameof(GetByIdAsync))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
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
    public async Task<ActionResult> CreateAsync([FromBody] TDto dto, CancellationToken cancellationToken)
    {
        if (dto == null)
            return BadRequest("O segmento do credor não foi informado.");

        var result = await service.CreateAsync(dto, cancellationToken);
        if (result == null)
            return BadRequest("Erro ao adicionar o segmento do credor.");

        return CreatedAtRoute(nameof(GetByIdAsync), new { id = result.Id }, result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IDto>> UpdateAsync(int id, [FromBody] TDto dto, CancellationToken cancellationToken)
    {
        if (dto == null)
            return BadRequest("O segmento do credor não foi informado.");

        if (dto.Id != id)
            return BadRequest("O ID do segmento do credor não corresponde ao ID da URL.");

        if (!await service.ExistsAsync(id, cancellationToken))
            return NotFound("O segmento do credor informado não existe.");

        var result = await service.UpdateAsync(dto, cancellationToken);
        if (result == null)
            return BadRequest("Erro ao atualizar o segmento do credor.");

        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        if (id <= 0)
            return BadRequest("O ID do segmento do credor deve ser maior que zero.");

        if (!await service.ExistsAsync(id, cancellationToken))
            return NotFound("O segmento do credor informado não existe.");

        var result = await service.DeleteAsync(id, cancellationToken);
        if (result)
            return NoContent();

        return BadRequest("Erro ao excluir o segmento do credor.");
    }
}

