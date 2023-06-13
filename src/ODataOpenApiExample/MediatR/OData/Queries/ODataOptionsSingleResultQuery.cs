namespace Shared.MediatR.OData.Queries;

using global::MediatR;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;

/// <summary>
/// 
/// </summary>
/// <param name="options"></param>
public sealed record ODataOptionsSingleResultQuery<T>(ODataQueryOptions<T> options) : IRequest<SingleResult<T>>;

