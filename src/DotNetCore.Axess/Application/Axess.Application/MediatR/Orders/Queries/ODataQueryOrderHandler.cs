using AutoMapper;
using MediatR;

namespace Axess.Application.MediatR.Orders.Queries;

using Axess.Application.MediatR.Queries;
using Axess.Domain.Entities;
using Axess.Domain.Repositories.Interfaces.Orders;

//using Order = ApiVersioning.Examples.Models.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataQueryOrderHandler : IRequestHandler<ODataQueryOrder, IQueryable<Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IOrderQueryRepository _orderReadRepository;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataQueryOrderHandler(IMapper mapper, IOrderQueryRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;

        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IQueryable<Order>> Handle(ODataQueryOrder request, CancellationToken cancellationToken)
    {
        IQueryable<Order> orders = _orderReadRepository.Queryable;
        ///Microsoft.EntityFrameworkCore.DbSet<Axess.Entities.Order> orders = _dbContext.Orders;
        ///int count = orders.Count();
        ///int count = _orderReadRepository.Queryable.Count();
        return orders;
        ///return await Task.FromResult(request.Options.ApplyTo(_mapper.ProjectTo<Order>(_dbContext.Orders)) as IEnumerable<Order>);
    }
}
