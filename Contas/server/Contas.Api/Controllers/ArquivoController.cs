using Contas.Api.Controllers.Base;
using Contas.Core.Dtos;
using Contas.Infrastructure.Services.Interfaces;

namespace Contas.Api.Controllers;

public class ArquivoController(IArquivoService service) : BaseApiController<ArquivoDto>(service)
{
}
