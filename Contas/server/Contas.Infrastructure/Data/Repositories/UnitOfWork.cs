using Contas.Core.Entities.Base;
using Contas.Core.Helpers;
using Contas.Core.Interfaces.Repositories;

namespace Contas.Infrastructure.Data.Repositories;

public class UnitOfWork(ContasContext context) : IUnitOfWork
{
    public IRepository<T> Repository<T>() where T : Entity
    {
        return FactoryHelper.CreateInstance<Repository<T>>(context);
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
