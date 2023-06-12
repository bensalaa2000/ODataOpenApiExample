using AutoMapper;
using Axess.Infrastructure.Persistence.Contexts;
using MediatR;
using ODataOpenApiExample.Extensions;
using ODataOpenApiExample.MediatR.OData.Queries;

namespace ODataOpenApiExample.MediatR.Order.Queries.OData.V4;
using Order = ApiVersioning.Examples.Models.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdIQueryableQueryHandler : IRequestHandler<ODataOptionsByIdIQueryableQuery<Order>, IQueryable<Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public ODataOptionsOrderByIdIQueryableQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IQueryable<Order>> Handle(ODataOptionsByIdIQueryableQuery<Order> request, CancellationToken cancellationToken)
    {
        IQueryable<Axess.Entities.Order> result = _dbContext.Orders.Where(o => o.Id == request.Key);
        IQueryable<Order> orders = result.ProjectAndApplyToIQueryable(_mapper, request.Options);
        return await Task.FromResult(orders);
    }
}