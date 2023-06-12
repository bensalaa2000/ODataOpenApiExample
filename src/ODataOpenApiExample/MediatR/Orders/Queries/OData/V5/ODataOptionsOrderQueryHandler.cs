using AutoMapper;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using ODataOpenApiExample.Extensions;
using ODataOpenApiExample.MediatR.OData.Queries;
using ODataOpenApiExample.Results;
namespace ODataOpenApiExample.MediatR.Order.Queries.OData.V5;

/// <inheritdoc/>
public sealed class ODataOptionsOrderQueryHandler : IRequestHandler<ODataOptionsQuery<DotNetCore.Axess.Entities.Order>, PaginatedList<DotNetCore.Axess.Entities.Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;
    private readonly HttpContext _context;
    private readonly IMapper _mapper;

    /// <inheritdoc/>
    public ODataOptionsOrderQueryHandler(IApplicationDbContext dbContext, HttpContext context, IMapper mapper)
    {
        _mapper = mapper;
        _dbContext = dbContext;
        _context = context;
    }

    /// <inheritdoc/>
    public async Task<PaginatedList<DotNetCore.Axess.Entities.Order>> Handle(ODataOptionsQuery<DotNetCore.Axess.Entities.Order> request, CancellationToken cancellationToken)
    {
        /*Automapper.ExpressionMappin sample*/
        /*
        ICollection<Models.Order> requests = await _dbContext.Orders.GetItemsAsync(_mapper, r => r.Id > 0 && r.Id < 3, null, new List<Expression<Func<IQueryable<Models.Order>, IIncludableQueryable<Models.Order, object>>>>() { item => item.Include(s => s.LineItems) });
        ICollection<Models.Order> users = await _dbContext.Orders.GetItemsAsync<Models.Order, Entities.Order>(_mapper, u => u.Id > 0 && u.Id < 4, q => q.OrderBy(u => u.Customer));
        int countByQuery = await _dbContext.Orders.Query<Models.Order, Entities.Order, int, int>(_mapper, q => q.Count(r => r.Id > 1));
        */
        /*Automapper.ExpressionMappin sample*/

        ODataQueryOptions<DotNetCore.Axess.Entities.Order> options = request.Options;
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
        IQueryable<DotNetCore.Axess.Entities.Order> items = request.Options.ApplyTo(_dbContext.Orders, querySettings) as IQueryable<DotNetCore.Axess.Entities.Order>;
        //IEnumerable<Order> items = _dbContext.Orders.ProjectAndApplyTo<Order>(_mapper, options, querySettings);

        PaginatedList<DotNetCore.Axess.Entities.Order> paginatedList = new(items, count, pageNumber, pageSize);
        return await Task.FromResult(paginatedList);
    }
}
