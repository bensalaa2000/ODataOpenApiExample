using AutoMapper;
using AutoMapper.AspNet.OData;
using MediatR;
using Microsoft.AspNetCore.OData.Query;
using ODataOpenApiExample.MediatR.OData.Queries;
using ODataOpenApiExample.Persistence.Contexts;

namespace ODataOpenApiExample.MediatR.Order.Queries.OData.V4;
using Order = ApiVersioning.Examples.Models.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderIEnumerableQueryHandler : IRequestHandler<ODataOptionsQueryOrder, IEnumerable<Order>>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataOptionsOrderIEnumerableQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<Order>> Handle(ODataOptionsQueryOrder request, CancellationToken cancellationToken)
    {
        int pageSize = request.Options.Top?.Value ?? request.PageSize;/* Taille pa defaut si n'est pas indiqué */
        QuerySettings querySettings = new QuerySettings { ODataSettings = new ODataSettings { HandleNullPropagation = HandleNullPropagationOption.False } };
        IQueryable<Order> result = await _dbContext.Orders.GetQueryAsync(_mapper, request.Options, querySettings);
        return result;
        //return await Task.FromResult(request.Options.ApplyTo(_mapper.ProjectTo<Order>(_dbContext.Orders)) as IEnumerable<Order>);
    }
}
