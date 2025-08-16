using Contas.Core.Entities.Base;

namespace Contas.Core.Interfaces;

public interface IConvertibleToEntity<TEntity> where TEntity : Entity
{
    TEntity ConvertToEntity();
}
