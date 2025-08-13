using Contas.Infrastructure.Services.Dtos;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SegmentoDoCredorController(ISegmentoDoCredorService service) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SegmentoDoCredorDto>>> GetAllAsync(CancellationToken cancellationToken)
        {
            var lista = await service.GetAllSegimentoDoCredorAsync(cancellationToken);

            if (lista == null || !lista.Any())
                return NotFound("Nenhum segmento do credor encontrado.");

            return Ok(lista);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SegmentoDoCredorDto>> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("O ID do segmento do credor deve ser maior que zero.");

            if (!await service.ExistsSegmentoDoCredorAsync(id, cancellationToken))
                return NotFound("O segmento do credor informado não existe.");

            var segmentoDoCredorDto = await service.GetSegmentoDoCredorByIdAsync(id, cancellationToken);
            if (segmentoDoCredorDto == null)
                return BadRequest("Erro ao buscar o segmento do credor.");

            return Ok(segmentoDoCredorDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromBody] SegmentoDoCredorDto segmentoDoCredorDto, CancellationToken cancellationToken)
        {
            if (segmentoDoCredorDto == null)
                return BadRequest("O segmento do credor não foi informado.");

            var result = await service.CreateSegmentoDoCredorAsync(segmentoDoCredorDto, cancellationToken);
            if (result == null)
                return BadRequest("Erro ao adicionar o segmento do credor.");

            return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SegmentoDoCredorDto>> UpdateAsync(int id, [FromBody] SegmentoDoCredorDto segmentoDoCredorDto, CancellationToken cancellationToken)
        {
            if (segmentoDoCredorDto == null)
                return BadRequest("O segmento do credor não foi informado.");

            if (segmentoDoCredorDto.Id != id)
                return BadRequest("O ID do segmento do credor não corresponde ao ID da URL.");

            if (!await service.ExistsSegmentoDoCredorAsync(id, cancellationToken))
                return NotFound("O segmento do credor informado não existe.");

            var result = await service.UpdateSegmentoDoCredorAsync(segmentoDoCredorDto, cancellationToken);
            if (result == null)
                return BadRequest("Erro ao atualizar o segmento do credor.");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
                return BadRequest("O ID do segmento do credor deve ser maior que zero.");

            // Verifica se o segmento do credor existe antes de tentar excluir
            {
                if (!await service.ExistsSegmentoDoCredorAsync(id, cancellationToken))
                    return NotFound("O segmento do credor informado não existe.");

                var result = await service.DeleteSegmentoDoCredorAsync(id, cancellationToken);
                if (result)
                    return NoContent();

                return BadRequest("Erro ao excluir o segmento do credor.");
            }
        }
    }
}
