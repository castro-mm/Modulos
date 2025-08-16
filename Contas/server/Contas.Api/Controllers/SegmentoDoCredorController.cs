using Contas.Api.Controllers.Base;
using Contas.Core.Dtos;
using Contas.Infrastructure.Services.Interfaces;

namespace Contas.Api.Controllers;

public class SegmentoDoCredorController(ISegmentoDoCredorService service) : BaseApiController<SegmentoDoCredorDto>(service)
{
    // This controller can be extended with specific functionality for SegmentoDoCredor
    // For example, you might want to add methods for specific queries or operations related to SegmentoDoCredor.
    // The base functionality is already provided by BaseApiController.
}
