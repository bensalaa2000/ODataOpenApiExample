using AutoMapper;
using Axess.Application.Models;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.Extensions;
using Axess.MediatR.OData.Queries;
using MediatR;
using Microsoft.AspNetCore.OData.Results;

namespace Axess.Application.MediatR.Orders.Queries.OData.V4;
using OrderDto = OrderDto;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrdeSingleResultQueryHandler : IRequestHandler<ODataOptionsSingleResultQuery<OrderDto>, SingleResult<OrderDto>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IOrderReadRepository orderReadRepository;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataOptionsOrdeSingleResultQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
    {
        this.orderReadRepository = orderReadRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<SingleResult<OrderDto>> Handle(ODataOptionsSingleResultQuery<OrderDto> request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Order> entities = orderReadRepository.Queryable;
        IQueryable<OrderDto> orders = entities.ProjectAndApplyToIQueryable(_mapper, request.options);
        SingleResult<OrderDto> singleResult = SingleResult.Create(orders);
        return await Task.FromResult(singleResult);

    }
}
