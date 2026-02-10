using Contas.Core.Objects;
using Contas.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Contas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("quantitativo-de-contas")]
    public async Task<IActionResult> ObterQuantitativoDeContasAsync()
    {
        var validationResult = new ValidationResult();

        var quantitativoDeContas = await _dashboardService.ObterQuantitativoDeContasAsync();
        
        if (quantitativoDeContas == null)
        {
            validationResult.AddError("DADOS_NAO_ENCONTRADOS", "Não foi possível obter os dados do dashboard.");
            return NotFound(Result.Failure(validationResult.Errors));
        }

        return Ok(Result.Successful(quantitativoDeContas));
    }
}
