using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.OData.Results;
using ODataOpenApiExample.Extensions;
using ODataOpenApiExample.MediatR.OData.Queries;
using ODataOpenApiExample.Persistence.Contexts;

namespace ODataOpenApiExample.MediatR.Order.Queries.OData.V4;
using Order = ApiVersioning.Examples.Models.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrdeSingleResultQueryHandler : IRequestHandler<ODataOptionsSingleResultQuery<Order>, SingleResult<Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataOptionsOrdeSingleResultQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<SingleResult<Order>> Handle(ODataOptionsSingleResultQuery<Order> request, CancellationToken cancellationToken)
    {
        IQueryable<Persistence.Entities.Order> entities = _dbContext.Orders;
        IQueryable<Order> orders = entities.ProjectAndApplyToIQueryable(_mapper, request.options);
        SingleResult<Order> singleResult = SingleResult.Create(orders);
        return await Task.FromResult(singleResult);

    }
}
