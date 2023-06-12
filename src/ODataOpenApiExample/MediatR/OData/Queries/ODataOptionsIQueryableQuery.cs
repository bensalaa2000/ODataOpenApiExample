namespace Axess.MediatR.OData.Queries;

using global::MediatR;
using Microsoft.AspNetCore.OData.Query;

/// <summary>
/// 
/// </summary>
/// <param name="Options"></param>
public sealed record ODataOptionsIQueryableQuery<T>(ODataQueryOptions<T> Options) : IRequest<IQueryable>;
