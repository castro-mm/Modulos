using System.Linq.Expressions;
using Contas.Core.Entities.Base;

namespace Contas.Core.Interfaces.Repositories;

public interface IRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task AddAsync(T entity, CancellationToken cancellationToken);
    void Update(T entity);
    void Delete(T entity);
    void DeleteRange(IEnumerable<T> entities);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);
    Task<IEnumerable<T>> GetAsyncWithSpec(ISpecification<T> spec, CancellationToken cancellationToken);
    Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken);
}
