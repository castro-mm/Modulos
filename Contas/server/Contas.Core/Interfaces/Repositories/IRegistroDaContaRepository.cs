using Contas.Core.Dtos.Dahsboard;
using Contas.Core.Entities;

namespace Contas.Core.Interfaces.Repositories;

public interface IRegistroDaContaRepository : IRepository<RegistroDaConta>
{
    Task<QuantitativoDeContasDto> ObterQuantitativoDeContasAsync(CancellationToken cancellationToken);
    Task<GastoMensalPorCredorDto> ObterGastoMensalPorCredorAsync(int ano, CancellationToken cancellationToken);
    Task<GastoPorSegmentoDoCredorDto> ObterGastoPorSegmentoDoCredorAsync(int ano, CancellationToken cancellationToken);
}
