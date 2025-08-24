using Contas.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TesteController(ContasContext context) : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var teste = context.Credores.Where(x => x.Id == 1).ToList();
            return Ok(teste);
        }
    }
}
