namespace ODataMappingApi.MediatR.Queries;
using global::MediatR;
using Microsoft.AspNetCore.OData.Query;
using Order = ApiVersioning.Examples.Models.Order;

/// <summary>
/// 
/// </summary>
/// <param name="Options"></param>
public sealed record ODataOptionsQuery<T>(ODataQueryOptions<T> Options, int PageSize = 10) : IRequest<IEnumerable<T>>;

public sealed record ODataOptionsQueryOrder(ODataQueryOptions<Order> Options, int PageSize = 10) : IRequest<IEnumerable<Order>>;
