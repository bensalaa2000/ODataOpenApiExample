using Axess.Infrastructure.Contexts;
using Axess.MediatR.OData.Queries;
using MediatR;

namespace Axess.MediatR.Order.Queries.OData.V5;
using Order = DotNetCore.Axess.Entities.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdIQueryableQueryHandler : IRequestHandler<ODataOptionsByIdIQueryableQuery<Order>, IQueryable<Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public ODataOptionsOrderByIdIQueryableQueryHandler(IApplicationDbContext context)
    {
        _dbContext = context;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IQueryable<Order>> Handle(ODataOptionsByIdIQueryableQuery<Order> request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(request.Options.ApplyTo(_dbContext.Orders) as IQueryable<Order>);
    }
}