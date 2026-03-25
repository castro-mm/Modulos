using Contas.Core.Dtos.Dahsboard;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Interfaces.Services;

namespace Contas.Infrastructure.Services.Dashboard;

public class DashboardService : IDashboardService
{
    private readonly IRegistroDaContaRepository _registroDaContaRepository;

    public DashboardService(IRegistroDaContaRepository registroDaContaRepository)
    {
        _registroDaContaRepository = registroDaContaRepository;
    }
    
    public async Task<QuantitativoDeContasDto> ObterQuantitativoDeContasAsync()
    {
        return await _registroDaContaRepository.ObterQuantitativoDeContasAsync(CancellationToken.None);
    }

    public async Task<GastoMensalPorCredorDto> ObterGastoMensalPorCredorAsync(int ano)
    {
        return await _registroDaContaRepository.ObterGastoMensalPorCredorAsync(ano, CancellationToken.None);
    }

    public async Task<GastoPorSegmentoDoCredorDto> ObterGastoPorSegmentoDoCredorAsync(int ano)
    {
        return await _registroDaContaRepository.ObterGastoPorSegmentoDoCredorAsync(ano, CancellationToken.None);
    }
}
