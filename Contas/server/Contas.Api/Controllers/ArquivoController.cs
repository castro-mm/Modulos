using Contas.Api.Controllers.Base;
using Contas.Core.Dtos;
using Contas.Core.Entities;
using Contas.Infrastructure.Services.Interfaces;

namespace Contas.Api.Controllers;

public class ArquivoController : BaseApiController<ArquivoDto, Arquivo>
{    
    public ArquivoController(IArquivoService service) : base(service)
    {
    }
}
