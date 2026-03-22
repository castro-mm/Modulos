using Contas.Core.Dtos.Dahsboard;

namespace Contas.Core.Interfaces.Services;

public interface IDashboardService
{
    Task<QuantitativoDeContasDto> ObterQuantitativoDeContasAsync();
}
