﻿using AutoMapper;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.OData.Results;
using Axess.Extensions;
using Axess.MediatR.OData.Queries;

namespace Axess.MediatR.Order.Queries.OData.V4;
using Order = Axess.Architecture.Models.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdQueryHandler : IRequestHandler<ODataOptionsByIdQuery<Order>, SingleResult<Order>>
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
    public async Task<SingleResult<Order>> Handle(ODataOptionsByIdQuery<Order> request, CancellationToken cancellationToken)
    {
        IQueryable<DotNetCore.Axess.Entities.Order> result = _dbContext.Orders.Where(o => o.Id == request.Key);
        IQueryable<Order> orders = result.ProjectAndApplyToIQueryable(_mapper, request.Options);
        SingleResult<Order> singleResult = SingleResult.Create(orders);
        SingleResult<Order> resultat = await Task.FromResult(singleResult);
        return resultat;
    }
}