using Axess.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ODataOpenApiExample.MediatR.OData.Queries;

namespace ODataOpenApiExample.MediatR.Order.Queries.OData.V5;
using Order = Axess.Entities.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdQueryHandler : IRequestHandler<ODataOptionsByIdQuery<Order>, SingleResult<Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public ODataOptionsOrderByIdQueryHandler(IApplicationDbContext context)
    {
        _dbContext = context;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<SingleResult<Order>> Handle(ODataOptionsByIdQuery<Order> request, CancellationToken cancellationToken)
    {
        IQueryable<Order> result = _dbContext.Orders.Where(o => o.Id == request.Key);
        IQueryable<Order> orders = request.Options.ApplyTo(_dbContext.Orders) as IQueryable<Order>;
        SingleResult<Order> singleResult = SingleResult.Create(orders);
        SingleResult<Order> resultat = await Task.FromResult(singleResult);
        return resultat;
    }
}