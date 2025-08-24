using System.Linq.Expressions;
using Contas.Core.Entities.Base;
using Contas.Core.Interfaces;

namespace Contas.Core.Specifications.Base;

public abstract class Specification<T> : ISpecification<T> where T : Entity
{
    public Expression<Func<T, bool>>? Criteria { get; private set; }
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public Expression<Func<T, object>>? GroupBy { get; private set; }
    public List<Expression<Func<T, object>>> Includes { get; } = [];
    public List<Expression<Func<T, object>>> ThenIncludes { get; } = [];
    public int? Skip { get; private set; }
    public int? Take { get; private set; }
    public bool AsNoTracking { get; private set; } = true;
    public bool IsDistinct { get; private set; } = false;
    public bool IsPageEnabled { get; private set; } = false;

    protected void AddCriteria(Expression<Func<T, bool>> criteria) => Criteria = criteria;
    protected void AddOrderBy(Expression<Func<T, object>> orderBy) => OrderBy = orderBy;
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescending) => OrderByDescending = orderByDescending;
    protected void AddGroupBy(Expression<Func<T, object>> groupBy) => GroupBy = groupBy;
    protected void AddInclude(Expression<Func<T, object>> include) => Includes.Add(include);
    protected void AddThenInclude(Expression<Func<T, object>> thenInclude) => ThenIncludes.Add(thenInclude);
    protected void ApplyAsNoTracking(bool asNoTracking = true) => AsNoTracking = asNoTracking;
    protected void ApplyIsDistinct(bool isDistinct = true) => IsDistinct = isDistinct;
    protected void ApplyPaging(int skip, int take) { IsPageEnabled = true; Skip = skip; Take = take; }    
}

