using Microsoft.AspNetCore.OData.Query;

namespace ODataOpenApiExample.Extensions;
/// <summary>
/// 
/// </summary>
public static class ODataQuerySettingsExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IEnumerable<T> ApplyFilter<T>(this IQueryable<T> query, ODataQueryOptions<T> options)
    {
        if (options.Filter == null)
        {
            return query;
        }
        return options.Filter.ApplyTo(query, new ODataQuerySettings()) as IEnumerable<T>;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static IEnumerable<T> ApplyTopAndTake<T>(this IEnumerable<T> query, ODataQueryOptions<T> options)
    {
        IEnumerable<T> value = query;

        if (options.Top != null)
        {
            value = value.Take(options.Top.Value);
        }

        if (options.Skip != null)
        {
            value = value.Skip(options.Skip.Value);
        }

        return value;
    }
}