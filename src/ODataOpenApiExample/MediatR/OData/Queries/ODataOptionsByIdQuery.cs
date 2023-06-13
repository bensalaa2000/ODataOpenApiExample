namespace Shared.MediatR.OData.Queries;

using global::MediatR;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="Options"></param>
/// <param name="Key"></param>
public sealed record ODataOptionsByIdQuery<T>(ODataQueryOptions<T> Options, Guid Key) : IRequest<SingleResult<T>>;