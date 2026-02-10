using Contas.Core.Dtos.Dahsboard;
using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Services.Interfaces;

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
}
