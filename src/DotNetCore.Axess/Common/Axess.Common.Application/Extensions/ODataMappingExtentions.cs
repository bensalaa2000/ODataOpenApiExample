using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.OData.Query;

namespace Axess.Extensions;
/// <summary>
/// 
/// </summary>
public static class ODataMappingExtentions
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entities"></param>
	/// <param name="_mapper"></param>
	/// <param name="queryOptions"></param>
	/// <returns></returns>
	public static IEnumerable<T> ApplyFilter<T>(this IQueryable entities,
		IMapper _mapper,
		ODataQueryOptions<T> queryOptions)
	{
		return entities.ProjectTo<T>(_mapper.ConfigurationProvider).ApplyFilter<T>(queryOptions);
	}
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entities"></param>
	/// <param name="_mapper"></param>
	/// <param name="queryOptions"></param>
	/// <returns></returns>
	public static int ApplyFilterCount<T>(this IQueryable entities,
		IMapper _mapper,
		ODataQueryOptions<T> queryOptions)
	{
		return entities.ApplyFilter<T>(_mapper, queryOptions).Count();
	}
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entities"></param>
	/// <param name="queryOptions"></param>
	/// <param name="querySettings"></param>
	/// <param name="_mapper"></param>
	/// <returns></returns>
	public static IEnumerable<T> ProjectAndApplyTo<T>(this IQueryable entities,
		IMapper _mapper,
		ODataQueryOptions<T> queryOptions,
		ODataQuerySettings? querySettings = null)
	{
		///IQueryable<T> dtos = _mapper.ProjectTo<T>(entities);
		var result = entities.ProjectTo<T>(_mapper.ConfigurationProvider);
		return (querySettings != null ? queryOptions.ApplyTo(result, querySettings) : queryOptions.ApplyTo(result)) as IEnumerable<T>;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="_mapper"></param>
	/// <param name="entities"></param>
	/// <param name="queryOptions"></param>
	/// <param name="querySettings"></param>
	/// <returns></returns>
	public static IEnumerable<T> ProjectAndApplyTo<T>(this IMapper _mapper,
		IQueryable entities,
		ODataQueryOptions<T> queryOptions,
		ODataQuerySettings querySettings)
	{
		var result = entities.ProjectTo<T>(_mapper.ConfigurationProvider);
		return (querySettings != null ? queryOptions.ApplyTo(result, querySettings) : queryOptions.ApplyTo(result)) as IEnumerable<T>;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="entities"></param>
	/// <param name="_mapper"></param>
	/// <param name="queryOptions"></param>
	/// <param name="querySettings"></param>
	/// <returns></returns>
	public static IQueryable<T> ProjectAndApplyToIQueryable<T>(this IQueryable entities,
		IMapper _mapper,
		ODataQueryOptions<T> queryOptions,
		ODataQuerySettings? querySettings = null) =>
			entities.ProjectAndApplyTo<T>(_mapper, queryOptions, querySettings).AsQueryable<T>();

	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="_mapper"></param>
	/// <param name="entities"></param>
	/// <param name="queryOptions"></param>
	/// <param name="querySettings"></param>
	/// <returns></returns>
	public static IQueryable<T> ProjectAndApplyToIQueryable<T>(this IMapper _mapper,
		IQueryable entities,
		ODataQueryOptions<T> queryOptions,
		ODataQuerySettings querySettings) =>
			_mapper.ProjectAndApplyTo<T>(entities, queryOptions, querySettings).AsQueryable<T>();
}
