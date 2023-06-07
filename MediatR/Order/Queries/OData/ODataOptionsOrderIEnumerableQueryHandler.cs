using AutoMapper;
using MediatR;
using ODataOpenApiExample.MediatR.OData.Queries;
using ODataOpenApiExample.Persistence.Contexts;
using System.Collections;

namespace ODataOpenApiExample.MediatR.Order.Queries.OData;
using Order = ApiVersioning.Examples.Models.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderIEnumerableQueryHandler : IRequestHandler<ODataOptionsIEnumerableQuery<Order>, IEnumerable>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataOptionsOrderIEnumerableQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._dbContext = context;
        this._mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable> Handle(ODataOptionsIEnumerableQuery<Order> request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(request.Options.ApplyTo(_mapper.ProjectTo<Order>(_dbContext.Orders)) as IEnumerable<Order>);
    }
}
