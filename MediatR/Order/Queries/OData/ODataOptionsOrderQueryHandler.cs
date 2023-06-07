using AutoMapper;
//using AutoMapper.AspNet.OData;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using ODataOpenApiExample.Extensions;
using ODataOpenApiExample.MediatR.OData.Queries;
using ODataOpenApiExample.Persistence.Contexts;
using ODataOpenApiExample.Results;

namespace ODataOpenApiExample.MediatR.Order.Queries.OData;
using Order = ApiVersioning.Examples.Models.Order;
/// <inheritdoc/>
public sealed class ODataOptionsOrderQueryHandler : IRequestHandler<ODataOptionsQuery<Order>, PaginatedList<Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataOptionsOrderQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._dbContext = context;
        this._mapper = mapper;
    }
    /// <inheritdoc/>
    public async Task<PaginatedList<Order>> Handle(ODataOptionsQuery<Order> request, CancellationToken cancellationToken)
    {
        ODataQueryOptions<Order> options = request.Options;
        /*Init PaginatedList header */
        int skip = options.Skip?.Value ?? 0;
        int pageSize = options.Top?.Value ?? request.PageSize;/* Taille pa defaut si n'est pas indiqué */
        int pageNumber = (int)Math.Ceiling((skip) / (double)pageSize) + 1;

        /* Nombre total d'elements avec Filter , sans 'Skip' ni 'Top' :  sans pagination */
        int count = _dbContext.Orders.ApplyFilterCount(_mapper, options);
        /* Resultat avec pagination */
        ODataQuerySettings querySettings = new()
        {
            PageSize = pageSize,
            EnsureStableOrdering = true,
        };
        IEnumerable<Order> items = _dbContext.Orders.ProjectAndApplyTo<Order>(_mapper, request.Options, querySettings);
        PaginatedList<Order> paginatedList = new(items, count, pageNumber, pageSize);
        return await Task.FromResult(paginatedList);
    }
}
