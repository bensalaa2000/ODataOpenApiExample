using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ODataOpenApiExample.Results;
using IConfigurationProvider = AutoMapper.IConfigurationProvider;

namespace ODataOpenApiExample.Extensions;
/// <summary>
/// 
/// </summary>
public static class MappingExtensions
{
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
