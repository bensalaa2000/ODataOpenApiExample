using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.MediatR.OData.Queries;
using MediatR;

namespace Axess.Application.MediatR.Orders.Queries.V5;
using Order = Domain.Entities.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdIQueryableQueryHandler : IRequestHandler<ODataOptionsByIdIQueryableQuery<Order>, IQueryable<Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IOrderReadRepository orderReadRepository;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public ODataOptionsOrderByIdIQueryableQueryHandler(IOrderReadRepository orderReadRepository)
    {
        this.orderReadRepository = orderReadRepository;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IQueryable<Order>> Handle(ODataOptionsByIdIQueryableQuery<Order> request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(request.Options.ApplyTo(orderReadRepository.Queryable) as IQueryable<Order>);
    }
}