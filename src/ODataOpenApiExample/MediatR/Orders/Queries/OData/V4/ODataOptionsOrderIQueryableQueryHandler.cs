using AutoMapper;
using Axess.MediatR.OData.Queries;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using MediatR;

namespace Axess.MediatR.Order.Queries.OData.V4;
using Order = Axess.Architecture.Models.Order;
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
        _dbContext = context;
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IQueryable> Handle(ODataOptionsIQueryableQuery<Order> request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(request.Options.ApplyTo(_mapper.ProjectTo<Order>(_dbContext.Orders)));
    }
}
