using AutoMapper;
using AutoMapper.AspNet.OData;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.Infrastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using ODataMappingApi.MediatR.Queries;

namespace ODataMappingApi.MediatR.Orders.Queries;
using Models = Axess.Application.Models;
//using Order = ApiVersioning.Examples.Models.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderIEnumerableQueryHandler : IRequestHandler<ODataOptionsQueryOrder, IEnumerable<Models.OrderDto>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IOrderQueryRepository _orderReadRepository;

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataOptionsOrderIEnumerableQueryHandler(IApplicationDbContext context, IMapper mapper, IOrderQueryRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
        _dbContext = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Models.OrderDto>> Handle(ODataOptionsQueryOrder request, CancellationToken cancellationToken)
    {
        /*Automapper.ExpressionMappin sample*/
        /*
        ICollection<Models.Order> requests = await _dbContext.Orders.GetItemsAsync(_mapper, r => r.Id > 0 && r.Id < 3, null, new List<Expression<Func<IQueryable<Models.Order>, IIncludableQueryable<Models.Order, object>>>>() { item => item.Include(s => s.LineItems) });
        ICollection<Models.Order> users = await _dbContext.Orders.GetItemsAsync<Models.Order, Entities.Order>(_mapper, u => u.Id > 0 && u.Id < 4, q => q.OrderBy(u => u.Customer));
        int countByQuery = await _dbContext.Orders.Query<Models.Order, Entities.Order>(_mapper, q => q.Count(r => r.Id > 1));
        */
        /*Automapper.ExpressionMappin sample*/


        int pageSize = request.Options.Top?.Value ?? request.PageSize;/* Taille pa defaut si n'est pas indiqué */
        QuerySettings querySettings = new QuerySettings { ODataSettings = new ODataSettings { HandleNullPropagation = HandleNullPropagationOption.False } };
        //IQueryable<Models.Order> result = await _dbContext.Orders.GetQueryAsync(_mapper, request.Options, querySettings);
        IQueryable<Models.OrderDto> result = await _orderReadRepository.Queryable.GetQueryAsync(_mapper, request.Options, querySettings);
        return result;
        //return await Task.FromResult(request.Options.ApplyTo(_mapper.ProjectTo<Order>(_dbContext.Orders)) as IEnumerable<Order>);
    }
}
