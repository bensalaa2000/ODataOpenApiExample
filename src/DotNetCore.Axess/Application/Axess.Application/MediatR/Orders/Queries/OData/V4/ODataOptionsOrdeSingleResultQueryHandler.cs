﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.OData.Results;
using Axess.Extensions;
using Axess.MediatR.OData.Queries;
using Axess.Infrastructure.Contexts;
using Axess.Application.Models;

namespace Axess.MediatR.Order.Queries.OData.V4;
using OrderDto = OrderDto;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrdeSingleResultQueryHandler : IRequestHandler<ODataOptionsSingleResultQuery<OrderDto>, SingleResult<OrderDto>>
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
    public async Task<SingleResult<OrderDto>> Handle(ODataOptionsSingleResultQuery<OrderDto> request, CancellationToken cancellationToken)
    {
        IQueryable<Domain.Entities.Order> entities = _dbContext.Orders;
        IQueryable<OrderDto> orders = entities.ProjectAndApplyToIQueryable(_mapper, request.options);
        SingleResult<OrderDto> singleResult = SingleResult.Create(orders);
        return await Task.FromResult(singleResult);

    }
}
