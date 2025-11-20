using System.Linq.Expressions;
using Contas.Core.Entities;
using Contas.Core.Entities.Base;
using Contas.Core.Interfaces;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Specifications.Base;
using Microsoft.EntityFrameworkCore;

namespace Contas.Infrastructure.Data.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly ContasContext _context;

    public Repository(ContasContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().FindAsync(id, cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public void Delete(T entity)
    {
        _context.Remove(entity);
    }

    public void Update(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void DeleteRange(IEnumerable<T> entities)
    {
        _context.Set<T>().RemoveRange(entities);
    }

    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().AnyAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAsyncWithSpec(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        var result = SpecificationEvaluator.GetQuery(_context.Set<T>().AsQueryable(), spec);

        return await result.ToListAsync(cancellationToken);
    }

    public async Task<int> CountAsync(ISpecification<T> spec, CancellationToken cancellationToken)
    {
        var query = _context.Set<T>().AsQueryable();

        query = spec.ApplyCriteria(query);

        return await query.CountAsync(cancellationToken);
    }
}
