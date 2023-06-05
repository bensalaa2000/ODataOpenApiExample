namespace ODataOpenApiExample.MediatR.OData.Queries;

using global::MediatR;
using Microsoft.AspNetCore.OData.Query;

/// <summary>
/// 
/// </summary>
/// <param name="options"></param>
public sealed record ODataOptionsQuery<T>(ODataQueryOptions<T> options) : IRequest<IQueryable>;