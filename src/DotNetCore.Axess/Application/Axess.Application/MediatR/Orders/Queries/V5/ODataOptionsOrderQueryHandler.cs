using AutoMapper;
using Axess.Domain.Entities;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.Extensions;
using Axess.MediatR.OData.Queries;
using Axess.Shared;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
//using Models = ApiVersioning.Examples.Models;
namespace Axess.Application.MediatR.Orders.Queries.V5;

/// <inheritdoc/>
public sealed class ODataOptionsOrderQueryHandler : IRequestHandler<ODataOptionsQuery<Order>, PaginatedList<Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IOrderReadRepository orderReadRepository;
    //private readonly HttpContext _context;
    private readonly IMapper _mapper;

    /// <inheritdoc/>
    public ODataOptionsOrderQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
    {
        this.orderReadRepository = orderReadRepository;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<PaginatedList<Order>> Handle(ODataOptionsQuery<Order> request, CancellationToken cancellationToken)
    {
        /*Automapper.ExpressionMappin sample*/
        /*
        ICollection<Models.Order> requests = await _dbContext.Orders.GetItemsAsync(_mapper, r => r.Id > 0 && r.Id < 3, null, new List<Expression<Func<IQueryable<Models.Order>, IIncludableQueryable<Models.Order, object>>>>() { item => item.Include(s => s.LineItems) });
        ICollection<Models.Order> users = await _dbContext.Orders.GetItemsAsync<Models.Order, Entities.Order>(_mapper, u => u.Id > 0 && u.Id < 4, q => q.OrderBy(u => u.Customer));
        int countByQuery = await _dbContext.Orders.Query<Models.Order, Entities.Order>(_mapper, q => q.Count(r => r.Id > 1));
        */
        /*Automapper.ExpressionMappin sample*/

        ODataQueryOptions<Order> options = request.Options;
        /*Init PaginatedList header */
        int skip = options.Skip?.Value ?? 0;
        int pageSize = options.Top?.Value ?? request.PageSize;/* Taille pa defaut si n'est pas indiqué */
        int pageNumber = (int)Math.Ceiling(skip / (double)pageSize) + 1;

        /* Nombre total d'elements avec Filter , sans 'Skip' ni 'Top' :  sans pagination */
        //int count = _dbContext.Orders.ApplyFilterCount(_mapper, options);
        int count = orderReadRepository.Queryable.ApplyFilterCount(options);
        /* Resultat avec pagination */
        ODataQuerySettings querySettings = new()
        {
            PageSize = pageSize,
            EnsureStableOrdering = true,
        };
        IQueryable<Order> items = request.Options.ApplyTo(orderReadRepository.Queryable, querySettings) as IQueryable<Order>;
        //IEnumerable<Models.Order> items = _dbContext.Orders.ProjectAndApplyTo<Models.Order>(_mapper, options, querySettings);

        PaginatedList<Order> paginatedList = new(items, count, pageNumber, pageSize);
        return await Task.FromResult(paginatedList);
    }
}
