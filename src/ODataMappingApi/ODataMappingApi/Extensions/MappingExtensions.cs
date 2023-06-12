using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Axess.Results;
using System.Linq.Expressions;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace ODataMappingApi.Extensions;
/// <summary>
/// 
/// </summary>
public static class MappingExtensions
{
    internal static async Task<TModelResult> Query<TModel, TData, TModelResult, TDataResult>(this IQueryable<TData> query, IMapper mapper,
            Expression<Func<IQueryable<TModel>, TModelResult>> queryFunc) where TData : class
    {
        //Map the expressions
        Func<IQueryable<TData>, TDataResult> mappedQueryFunc = mapper.MapExpression<Expression<Func<IQueryable<TData>, TDataResult>>>(queryFunc).Compile();

        //execute the query
        return mapper.Map<TDataResult, TModelResult>(mappedQueryFunc(query));
    }

    internal static async Task<ICollection<TModel>> GetItemsAsync<TModel, TData>(this IQueryable<TData> query, IMapper mapper,
        Expression<Func<TModel, bool>> filter = null,
        Expression<Func<IQueryable<TModel>, IQueryable<TModel>>> queryFunc = null,
        ICollection<Expression<Func<IQueryable<TModel>, IIncludableQueryable<TModel, object>>>> includeProperties = null)
    {
        //Map the expressions
        Expression<Func<TData, bool>> f = mapper.MapExpression<Expression<Func<TData, bool>>>(filter);
        Func<IQueryable<TData>, IQueryable<TData>> mappedQueryFunc = mapper.MapExpression<Expression<Func<IQueryable<TData>, IQueryable<TData>>>>(queryFunc)?.Compile();
        ICollection<Expression<Func<IQueryable<TData>, IIncludableQueryable<TData, object>>>> includes = mapper.MapIncludesList<Expression<Func<IQueryable<TData>, IIncludableQueryable<TData, object>>>>(includeProperties);

        if (f != null)
            query = query.Where(f);

        if (includes != null)
            query = includes.Select(i => i.Compile()).Aggregate(query, (list, next) => query = next(query));

        //Call the store
        ICollection<TData> result = mappedQueryFunc != null ? await mappedQueryFunc(query).ToListAsync() : await query.ToListAsync();

        //Map and return the data
        return mapper.Map<IEnumerable<TData>, IEnumerable<TModel>>(result).ToList();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="count"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int count, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), count, pageNumber, pageSize);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="queryable"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();
}
