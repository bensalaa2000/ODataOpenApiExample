namespace Axess.MediatR.OData.Queries;

using global::MediatR;
using Microsoft.AspNetCore.OData.Query;
using System.Collections;

/// <summary>
/// 
/// </summary>
/// <param name="Options"></param>
public sealed record ODataOptionsIEnumerableQuery<T>(ODataQueryOptions<T> Options) : IRequest<IEnumerable>;
