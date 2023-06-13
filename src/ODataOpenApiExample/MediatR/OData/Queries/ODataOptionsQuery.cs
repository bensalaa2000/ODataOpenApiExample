namespace Shared.MediatR.OData.Queries;

using global::MediatR;
using Microsoft.AspNetCore.OData.Query;
using Shared.Dto;

/// <summary>
///  Used on V4 and V5 controllers
/// </summary>
/// <param name="Options"></param>
public sealed record ODataOptionsQuery<T>(ODataQueryOptions<T> Options, int PageSize = 10) : IRequest<PaginatedList<T>>;
