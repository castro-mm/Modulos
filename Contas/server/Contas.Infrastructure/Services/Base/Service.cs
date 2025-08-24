using Contas.Core.Entities.Base;
using Contas.Core.Interfaces;
using Contas.Core.Interfaces.Repositories;
using Contas.Infrastructure.Services.Interfaces;

namespace Contas.Infrastructure.Services.Base;

public abstract class Service<TDto, TEntity>(IUnitOfWork unitOfWork) : IService<TDto, TEntity> 
    where TDto : IDto, IConvertibleToEntity<TEntity>
    where TEntity : Entity, IConvertibleToDto<TDto>
{
    public virtual async Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var lista = await unitOfWork.Repository<TEntity>().GetAllAsync(cancellationToken);

        return lista.Select(x => x.ConvertToDto());
    }

    public async virtual Task<TDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<TEntity>().GetByIdAsync(id, cancellationToken);

        if (entity == null)
            return default!;

        return entity.ConvertToDto();        
    }

    public virtual async Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ConvertToEntity();

        await unitOfWork.Repository<TEntity>().AddAsync(entity, cancellationToken);

        await unitOfWork.SaveAllAsync();

        return entity.ConvertToDto();
    }

    public virtual async Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var existingEntity = await unitOfWork.Repository<TEntity>().GetByIdAsync(dto.Id, cancellationToken);

        if (existingEntity == null)
            return default!;

        existingEntity.ConvertFromDto(dto);

        unitOfWork.Repository<TEntity>().Update(existingEntity);

        await unitOfWork.SaveAllAsync();

        return existingEntity.ConvertToDto();
    }

    public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await unitOfWork.Repository<TEntity>().GetByIdAsync(id, cancellationToken);

        if (entity == null)
            return false;

        unitOfWork.Repository<TEntity>().Delete(entity);

        return await unitOfWork.SaveAllAsync();
    }

    public virtual async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await unitOfWork.Repository<TEntity>().ExistsAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<TDto>> GetAsyncWithSpec(ISpecification<TEntity> spec, CancellationToken cancellationToken)
    {
        var lista = await unitOfWork.Repository<TEntity>().GetAsyncWithSpec(spec, cancellationToken);

        return lista.Select(x => x.ConvertToDto());
    }
}

