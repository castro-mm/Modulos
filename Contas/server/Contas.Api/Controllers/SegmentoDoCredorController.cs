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
        public async Task<ActionResult<IEnumerable<SegmentoDoCredorDto>>> GetAll()
        {
            var lista = await service.GetAllSegimentoDoCredorAsync();

            if (lista == null || !lista.Any())
                return NotFound("Nenhum segmento do credor encontrado.");

            return Ok(lista);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SegmentoDoCredorDto>> GetById(int id)
        {
            if (id <= 0)
                return BadRequest("O ID do segmento do credor deve ser maior que zero.");

            if (!await service.ExistsSegmentoDoCredorAsync(id))
                return NotFound("O segmento do credor informado não existe.");

            var segmentoDoCredorDto = await service.GetSegmentoDoCredorByIdAsync(id);
            if (segmentoDoCredorDto == null)
                return BadRequest("Erro ao buscar o segmento do credor.");

            return Ok(segmentoDoCredorDto);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] SegmentoDoCredorDto segmentoDoCredorDto)
        {
            if (segmentoDoCredorDto == null)
                return BadRequest("O segmento do credor não foi informado.");

            var result = await service.CreateSegmentoDoCredorAsync(segmentoDoCredorDto);
            if (result == null)
                return BadRequest("Erro ao adicionar o segmento do credor.");

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<SegmentoDoCredorDto>> Update(int id, [FromBody] SegmentoDoCredorDto segmentoDoCredorDto)
        {
            if (segmentoDoCredorDto == null)
                return BadRequest("O segmento do credor não foi informado.");

            if (segmentoDoCredorDto.Id != id)
                return BadRequest("O ID do segmento do credor não corresponde ao ID da URL.");

            if (!await service.ExistsSegmentoDoCredorAsync(id))
                return NotFound("O segmento do credor informado não existe.");

            var result = await service.UpdateSegmentoDoCredorAsync(segmentoDoCredorDto);
            if (result == null)
                return BadRequest("Erro ao atualizar o segmento do credor.");

            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            if (!await service.ExistsSegmentoDoCredorAsync(id))
                return NotFound("O segmento do credor informado não existe.");

            var result = await service.DeleteSegmentoDoCredorAsync(id);
            if (result)
                return NoContent();

            return BadRequest("Erro ao excluir o segmento do credor.");
        }

        [HttpGet("exists/{id:int}")]
        public async Task<ActionResult<bool>> Exists(int id)
        {
            if (id <= 0)
                return BadRequest("O ID do segmento do credor deve ser maior que zero.");

            return await service.ExistsSegmentoDoCredorAsync(id)
                ? Ok(true)
                : NotFound(false);                
        }
    }
}
