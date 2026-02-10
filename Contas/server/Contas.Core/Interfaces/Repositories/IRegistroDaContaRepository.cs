using Contas.Core.Dtos.Dahsboard;
using Contas.Core.Entities;

namespace Contas.Core.Interfaces.Repositories;

public interface IRegistroDaContaRepository : IRepository<RegistroDaConta>
{
    Task<QuantitativoDeContasDto> ObterQuantitativoDeContasAsync(CancellationToken cancellationToken);
}
