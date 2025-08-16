using Contas.Core.Interfaces;

namespace Contas.Infrastructure.Services.Interfaces;

public interface IService<TDto> where TDto : IDto
{
    Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<TDto> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken);
    Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
}

