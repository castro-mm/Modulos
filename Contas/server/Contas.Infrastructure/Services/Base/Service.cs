using Contas.Core.Entities.Base;
using Contas.Core.Interfaces;
using Contas.Core.Interfaces.Repositories;
using Contas.Core.Objects;
using Contas.Infrastructure.Services.Interfaces;

namespace Contas.Infrastructure.Services.Base;

public abstract class Service<TDto, TEntity> : IService<TDto, TEntity>
    where TDto : IDto, IConvertibleToEntity<TEntity>
    where TEntity : Entity, IConvertibleToDto<TDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public Service(IUnitOfWork unitOfWork)
    {        
        _unitOfWork = unitOfWork;
    }

    public async Task<PagedResult<TDto>> GetPagedResultWithSpecAsync(ISpecification<TEntity> spec, int pageIndex, int pageSize, CancellationToken cancellationToken)
    {
        var itens = await _unitOfWork.Repository<TEntity>().GetAsyncWithSpec(spec, cancellationToken);
        var count = await _unitOfWork.Repository<TEntity>().CountAsync(spec, cancellationToken);

        var pagination = new PagedResult<TDto>(pageIndex, pageSize, count, itens.Select(x => x.ConvertToDto()));

        return pagination;
    }

    public virtual async Task<IEnumerable<TDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var lista = await _unitOfWork.Repository<TEntity>().GetAllAsync(cancellationToken);

        return lista.Select(x => x.ConvertToDto());
    }

    public async virtual Task<TDto> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id, cancellationToken);

        if (entity == null)
            return default!;

        return entity.ConvertToDto();
    }

    public virtual async Task<TDto> CreateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var entity = dto.ConvertToEntity();

        await _unitOfWork.Repository<TEntity>().AddAsync(entity, cancellationToken);

        await _unitOfWork.SaveAllAsync();

        return entity.ConvertToDto();
    }

    public virtual async Task<TDto> UpdateAsync(TDto dto, CancellationToken cancellationToken)
    {
        var existingEntity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(dto.Id, cancellationToken);

        if (existingEntity == null)
            return default!;

        dto.DataDeAtualizacao = DateTime.Now;
        existingEntity.ConvertFromDto(dto);

        _unitOfWork.Repository<TEntity>().Update(existingEntity);

        await _unitOfWork.SaveAllAsync();

        return existingEntity.ConvertToDto();
    }

    public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var entity = await _unitOfWork.Repository<TEntity>().GetByIdAsync(id, cancellationToken);

        if (entity == null)
            return false;

        _unitOfWork.Repository<TEntity>().Delete(entity);

        return await _unitOfWork.SaveAllAsync();
    }

    public virtual async Task<bool> DeleteRangeAsync(IEnumerable<int> ids, CancellationToken cancellationToken)
    {
        var entities = await _unitOfWork.Repository<TEntity>().FindAsync(x => ids.Contains(x.Id), cancellationToken);

        if (!entities.Any())
            return false;

        _unitOfWork.Repository<TEntity>().DeleteRange(entities);

        return await _unitOfWork.SaveAllAsync();
    }

    public virtual async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken)
    {
        return await _unitOfWork.Repository<TEntity>().ExistsAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<TDto>> GetAsyncWithSpec(ISpecification<TEntity> spec, CancellationToken cancellationToken)
    {
        var lista = await _unitOfWork.Repository<TEntity>().GetAsyncWithSpec(spec, cancellationToken);

        return lista.Select(x => x.ConvertToDto());
    }
}

