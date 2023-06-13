﻿using AutoMapper;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using Shared.Dto;
using Shared.Extensions;
using Shared.MediatR.OData.Queries;

namespace Shared.MediatR.Order.Queries.OData.V4;
using OrderDto = ApiVersioning.Examples.Models.OrderDto;
/// <inheritdoc/>
public sealed class ODataOptionsOrderQueryHandler : IRequestHandler<ODataOptionsQuery<OrderDto>, PaginatedList<OrderDto>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataOptionsOrderQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }
    /// <inheritdoc/>
    public async Task<PaginatedList<OrderDto>> Handle(ODataOptionsQuery<OrderDto> request, CancellationToken cancellationToken)
    {
        ODataQueryOptions<OrderDto> options = request.Options;
        /*Init PaginatedList header */
        int skip = options.Skip?.Value ?? 0;
        int pageSize = options.Top?.Value ?? request.PageSize;/* Taille pa defaut si n'est pas indiqué */
        int pageNumber = (int)Math.Ceiling(skip / (double)pageSize) + 1;

        /* Nombre total d'elements avec Filter , sans 'Skip' ni 'Top' :  sans pagination */
        int count = _dbContext.Orders.ApplyFilterCount(_mapper, options);
        /* Resultat avec pagination */
        ODataQuerySettings querySettings = new()
        {
            PageSize = pageSize,
            EnsureStableOrdering = true,
        };
        IEnumerable<OrderDto> items = _dbContext.Orders.ProjectAndApplyTo(_mapper, options, querySettings);
        PaginatedList<OrderDto> paginatedList = new(items, count, pageNumber, pageSize);
        return await Task.FromResult(paginatedList);
    }
}
