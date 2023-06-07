using AutoMapper;
using MediatR;
using ODataOpenApiExample.MediatR.OData.Queries;
using ODataOpenApiExample.Persistence.Contexts;

namespace ODataOpenApiExample.MediatR.Order.Queries.OData;
using Order = ApiVersioning.Examples.Models.Order;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderIQueryableQueryHandler : IRequestHandler<ODataOptionsIQueryableQuery<Order>, IQueryable>
{
    //https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <inheritdoc/>
    public ODataOptionsOrderIQueryableQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._dbContext = context;
        this._mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IQueryable> Handle(ODataOptionsIQueryableQuery<Order> request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(request.Options.ApplyTo(_mapper.ProjectTo<Order>(_dbContext.Orders)));
    }
}
