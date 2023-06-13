using DotNetCore.Axess.Domain;
using DotNetCore.Axess.Specification.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Axess.Specification;

public static class SpecificationEvaluator<TEntity, TId> where TEntity : Base<Entity<TId>>
{
    public static async Task<IQueryable<TEntity>> GetQueryAsync(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
    {
        return await Task.FromResult(GetQuery(inputQuery, specification));
    }

    public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
    {
        IQueryable<TEntity> query = inputQuery;

        // modify the IQueryable using the specification's criteria expression
        if (specification.Criteria != null)
        {
            query = query.Where(specification.Criteria);
        }

        // Includes all expression-based includes
        query = specification.Includes.Aggregate(query,
                                (current, include) => current.Include(include));

        // Include any string-based include statements
        query = specification.IncludeStrings.Aggregate(query,
                                (current, include) => current.Include(include));

        // Apply ordering if expressions are set
        if (specification.OrderBy != null)
        {
            query = query.OrderBy(specification.OrderBy);
        }
        else if (specification.OrderByDescending != null)
        {
            query = query.OrderByDescending(specification.OrderByDescending);
        }

        if (specification.GroupBy != null)
        {
            query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
        }

        // Apply paging if enabled
        if (specification.IsPagingEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }
        return query;
    }
}
