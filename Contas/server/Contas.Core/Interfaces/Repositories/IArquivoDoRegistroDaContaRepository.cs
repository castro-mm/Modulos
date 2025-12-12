using Contas.Core.Entities;

namespace Contas.Core.Interfaces.Repositories;

public interface IArquivoDoRegistroDaContaRepository : IRepository<ArquivoDoRegistroDaConta>
{
    Task<List<ArquivoDoRegistroDaConta>> FindByRegistroDaContaIdAsync(int registroDaContaId, CancellationToken cancellationToken);
}
