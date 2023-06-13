using AutoMapper;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.OData.Results;
using Shared.Extensions;
using Shared.MediatR.OData.Queries;

namespace Shared.MediatR.Order.Queries.OData.V4;
using OrderDto = ApiVersioning.Examples.Models.OrderDto;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdQueryHandler : IRequestHandler<ODataOptionsByIdQuery<OrderDto>, SingleResult<OrderDto>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public ODataOptionsOrderByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
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
    public async Task<SingleResult<OrderDto>> Handle(ODataOptionsByIdQuery<OrderDto> request, CancellationToken cancellationToken)
    {
        IQueryable<DotNetCore.Axess.Entities.Order> result = _dbContext.Orders.Where(o => o.Id == request.Key);
        IQueryable<OrderDto> orders = result.ProjectAndApplyToIQueryable(_mapper, request.Options);
        SingleResult<OrderDto> singleResult = SingleResult.Create(orders);
        SingleResult<OrderDto> resultat = await Task.FromResult(singleResult);
        return resultat;
    }
}