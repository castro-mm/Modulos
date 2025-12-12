using Contas.Core.Entities;
using Contas.Core.Entities.Base;

namespace Contas.Core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<ArquivoDoRegistroDaConta> ArquivoDoRegistroDaContaRepository { get; }
    IRepository<T> Repository<T>() where T : Entity;
    Task<bool> SaveAllAsync();
}
