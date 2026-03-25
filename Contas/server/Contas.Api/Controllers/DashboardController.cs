using Contas.Core.Interfaces.Services;
using Contas.Core.Objects;
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

    [HttpGet("gasto-mensal-por-credor")]
    public async Task<IActionResult> ObterGastoMensalPorCredorAsync([FromQuery] int? ano)
    {
        var validationResult = new ValidationResult();

        var anoFiltro = ano ?? DateTime.Now.Year;

        var gastoMensalPorCredor = await _dashboardService.ObterGastoMensalPorCredorAsync(anoFiltro);

        if (gastoMensalPorCredor == null)
        {
            validationResult.AddError("DADOS_NAO_ENCONTRADOS", "Não foi possível obter os dados de gastos mensais por credor.");
            return NotFound(Result.Failure(validationResult.Errors));
        }

        return Ok(Result.Successful(gastoMensalPorCredor));
    }

    [HttpGet("gasto-por-segmento-do-credor")]
    public async Task<IActionResult> ObterGastoPorSegmentoDoCredorAsync([FromQuery] int? ano)
    {
        var validationResult = new ValidationResult();

        var anoFiltro = ano ?? DateTime.Now.Year;

        var gastoPorSegmento = await _dashboardService.ObterGastoPorSegmentoDoCredorAsync(anoFiltro);

        if (gastoPorSegmento == null)
        {
            validationResult.AddError("DADOS_NAO_ENCONTRADOS", "Não foi possível obter os dados de gastos por segmento do credor.");
            return NotFound(Result.Failure(validationResult.Errors));
        }

        return Ok(Result.Successful(gastoPorSegmento));
    }
}
