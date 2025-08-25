using Contas.Core.Entities.Base;
using Contas.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Contas.Core.Specifications.Base;

public static class SpecificationEvaluator
{
    public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, ISpecification<T> spec) where T : Entity
    {
        var query = inputQuery;

        query = spec.Criteria != null ? query.Where(spec.Criteria) : query;
        query = spec.OrderBy != null ? query.OrderBy(spec.OrderBy) : query;
        query = spec.OrderByDescending != null ? query.OrderByDescending(spec.OrderByDescending) : query;
        query = spec.GroupBy != null ? query.GroupBy(spec.GroupBy).SelectMany(g => g) : query;
        query = spec.IsDistinct ? query.Distinct() : query;
        query = spec.AsNoTracking ? query.AsNoTracking() : query;
        query = spec.IsPageEnabled ? query.Skip(spec.Skip).Take(spec.Take) : query;
        query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

        return query;
    }
}
