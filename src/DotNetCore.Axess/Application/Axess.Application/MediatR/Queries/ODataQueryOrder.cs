namespace Axess.Application.MediatR.Queries;
using global::MediatR;
//using DotNetCore.Axess.Entities;
/// <summary>
/// 
/// </summary>
/// <param name="Options"></param>
public sealed record ODataQueryOrder(int Size) : IRequest<IQueryable<Domain.Entities.Order>>;
