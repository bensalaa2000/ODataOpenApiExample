namespace ODataMappingApi.MediatR.Queries;
using global::MediatR;
//using DotNetCore.Axess.Entities;
/// <summary>
/// 
/// </summary>
/// <param name="Options"></param>
public sealed record ODataQueryOrder(int Size) : IRequest<IQueryable<Axess.Domain.Entities.Order>>;
