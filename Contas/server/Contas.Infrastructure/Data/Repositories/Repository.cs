using System.Linq.Expressions;
using Contas.Core.Entities.Base;
using Contas.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data.Repositories;

public class Repository<T>(ContasContext context) : IRepository<T> where T : Entity
{
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Delete(T entity)
    {
        context.Remove(entity);
    }

    public void Update(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Set<T>().AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }
}
