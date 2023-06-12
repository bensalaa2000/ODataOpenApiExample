namespace Axess.MediatR.OData.Queries;

using global::MediatR;
using Microsoft.AspNetCore.OData.Query;
using Axess.Results;

/// <summary>
/// 
/// </summary>
/// <param name="Options"></param>
public sealed record ODataOptionsQuery<T>(ODataQueryOptions<T> Options, int PageSize = 10) : IRequest<PaginatedList<T>>;
