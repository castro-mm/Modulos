using System.Collections.Concurrent;
using Contas.Core.Entities.Base;
using Contas.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data.Repositories;

public class UnitOfWork(DbContext context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();

    public IRepository<T> Repository<T>() where T : Entity
    {
        var type = typeof(T).Name;
        return (IRepository<T>)_repositories.GetOrAdd(type, t =>
        {
            var repositoryType = typeof(Repository<>).MakeGenericType(typeof(T));
            return Activator.CreateInstance(repositoryType, context) ?? throw new InvalidOperationException($"Não foi possível criar a instancia de {t}");
        });
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
    public void Dispose()
    {
        context.Dispose();
    }
}
