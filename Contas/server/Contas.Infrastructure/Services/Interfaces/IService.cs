using Contas.Core.Entities.Base;
using Contas.Core.Helpers;
using Contas.Core.Interfaces;
using Contas.Core.Objects;

namespace Contas.Infrastructure.Services.Interfaces;

public interface IService<TDto, TEntity>
    where TDto : IDto
    where TEntity : Entity
{
    Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<TDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PagedResult<TDto>> GetPagedResultWithSpecAsync(ISpecification<TEntity> spec, int pageIndex, int pageSize, CancellationToken cancellationToken);
    Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken);
    Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<bool> DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TDto>> GetAsyncWithSpec(ISpecification<TEntity> spec, CancellationToken cancellationToken);
}

