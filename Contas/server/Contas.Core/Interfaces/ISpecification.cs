using System.Linq.Expressions;
using Contas.Core.Entities.Base;

namespace Contas.Core.Interfaces;

public interface ISpecification<T> where T : Entity
{    
    Expression<Func<T, bool>>? Criteria { get; }
    Expression<Func<T, object>>? OrderBy { get; }
    Expression<Func<T, object>>? OrderByDescending { get; }
    Expression<Func<T, object>>? GroupBy { get; }
    List<Expression<Func<T, object>>> Includes { get; }
    List<Expression<Func<T, object>>> ThenIncludes { get; }
    int Skip { get; }
    int Take { get; }
    bool AsNoTracking { get; }
    bool IsDistinct { get; }
    bool IsPageEnabled { get; }

    IQueryable<T> ApplyCriteria(IQueryable<T> query);
}