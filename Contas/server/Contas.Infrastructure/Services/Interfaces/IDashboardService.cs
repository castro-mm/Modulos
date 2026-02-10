using Contas.Core.Dtos.Dahsboard;

namespace Contas.Infrastructure.Services.Interfaces;

public interface IDashboardService
{
    Task<QuantitativoDeContasDto> ObterQuantitativoDeContasAsync();
}
