using Contas.Core.Entities.Base;

namespace Contas.Core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : Entity;
    Task<bool> SaveAllAsync();
}
