namespace ODataOpenApiExample.MediatR.OData.Queries;

using global::MediatR;
using Microsoft.AspNetCore.OData.Query;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Options"></param>
/// <param name="Key"></param>
public sealed record ODataOptionsByIdIQueryableQuery<T>(ODataQueryOptions<T> Options, int Key) : IRequest<IQueryable<T>>;