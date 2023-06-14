using AutoMapper;
using MediatR;
using Axess.Extensions;
using Axess.MediatR.OData.Queries;
using Axess.Infrastructure.Contexts;

namespace Axess.MediatR.Order.Queries.OData.V4;
using OrderDto = ApiVersioning.Examples.Models.OrderDto;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdIQueryableQueryHandler : IRequestHandler<ODataOptionsByIdIQueryableQuery<OrderDto>, IQueryable<OrderDto>>
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
    public async Task<IQueryable<OrderDto>> Handle(ODataOptionsByIdIQueryableQuery<OrderDto> request, CancellationToken cancellationToken)
    {
        IQueryable<DotNetCore.Axess.Entities.Order> result = _dbContext.Orders.Where(o => o.Id == request.Key);
        IQueryable<OrderDto> orders = result.ProjectAndApplyToIQueryable(_mapper, request.Options);
        return await Task.FromResult(orders);
    }
}