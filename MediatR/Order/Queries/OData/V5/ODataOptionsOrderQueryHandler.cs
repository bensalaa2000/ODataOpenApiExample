using MediatR;
using Microsoft.AspNetCore.OData.Query;
using ODataOpenApiExample.Extensions;
using ODataOpenApiExample.MediatR.OData.Queries;
using ODataOpenApiExample.Persistence.Contexts;
using ODataOpenApiExample.Results;
using Entities = ODataOpenApiExample.Persistence.Entities;


namespace ODataOpenApiExample.MediatR.Order.Queries.OData.V5;

/// <inheritdoc/>
public sealed class ODataOptionsOrderQueryHandler : IRequestHandler<ODataOptionsQuery<Entities.Order>, PaginatedList<Entities.Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    /// <inheritdoc/>
    public ODataOptionsOrderQueryHandler(IApplicationDbContext context)
    {
        _dbContext = context;

    }
    /// <inheritdoc/>
    public async Task<PaginatedList<Entities.Order>> Handle(ODataOptionsQuery<Entities.Order> request, CancellationToken cancellationToken)
    {



        ODataQueryOptions<Entities.Order> options = request.Options;
        /*Init PaginatedList header */
        int skip = options.Skip?.Value ?? 0;
        int pageSize = options.Top?.Value ?? request.PageSize;/* Taille pa defaut si n'est pas indiqué */
        int pageNumber = (int)Math.Ceiling(skip / (double)pageSize) + 1;

        /* Nombre total d'elements avec Filter , sans 'Skip' ni 'Top' :  sans pagination */
        //int count = _dbContext.Orders.ApplyFilterCount(_mapper, options);
        int count = _dbContext.Orders.ApplyFilterCount(options);
        /* Resultat avec pagination */
        ODataQuerySettings querySettings = new()
        {
            PageSize = pageSize,
            EnsureStableOrdering = true,
        };
        IQueryable<Entities.Order> items = request.Options.ApplyTo(_dbContext.Orders, querySettings) as IQueryable<Entities.Order>;
        //IEnumerable<Order> items = _dbContext.Orders.ProjectAndApplyTo<Order>(_mapper, options, querySettings);

        PaginatedList<Entities.Order> paginatedList = new(items, count, pageNumber, pageSize);
        return await Task.FromResult(paginatedList);
    }
}
