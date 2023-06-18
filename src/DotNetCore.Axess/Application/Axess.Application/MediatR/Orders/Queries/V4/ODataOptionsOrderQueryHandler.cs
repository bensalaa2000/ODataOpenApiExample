using AutoMapper;
using Axess.Application.Models;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.Extensions;
using Axess.MediatR.OData.Queries;
using Axess.Shared;
using MediatR;
using Microsoft.AspNetCore.OData.Query;

namespace Axess.Application.MediatR.Orders.Queries.V4;
using OrderDto = OrderDto;
/// <inheritdoc/>
public sealed class ODataOptionsOrderQueryHandler : IRequestHandler<ODataOptionsQuery<OrderDto>, PaginatedList<OrderDto>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IOrderReadRepository orderReadRepository;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataOptionsOrderQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
    {
        this.orderReadRepository = orderReadRepository;
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
        int count = orderReadRepository.Queryable.ApplyFilterCount(_mapper, options);
        /* Resultat avec pagination */
        ODataQuerySettings querySettings = new()
        {
            PageSize = pageSize,
            EnsureStableOrdering = true,
        };
        /*Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Domain.Entities.Order, ICollection<Domain.Entities.LineItem>> entities = _dbContext.Orders.Include(x => x.LineItems);
        IQueryable<OrderDto> dtos = _mapper.ProjectTo<OrderDto>(entities);*/

        IEnumerable<OrderDto> items = orderReadRepository.Queryable.ProjectAndApplyTo(_mapper, options, querySettings);
        PaginatedList<OrderDto> paginatedList = new(items, count, pageNumber, pageSize);
        return await Task.FromResult(paginatedList);
    }
}
