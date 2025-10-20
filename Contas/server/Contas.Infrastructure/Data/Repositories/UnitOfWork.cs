using Contas.Core.Entities.Base;
using Contas.Core.Helpers;
using Contas.Core.Interfaces.Repositories;

namespace Contas.Infrastructure.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ContasContext _context;

    public UnitOfWork(ContasContext context)
    {
        _context = context;
    }

    public IRepository<T> Repository<T>() where T : Entity
    {
        return FactoryHelper.CreateInstance<Repository<T>>(_context);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
