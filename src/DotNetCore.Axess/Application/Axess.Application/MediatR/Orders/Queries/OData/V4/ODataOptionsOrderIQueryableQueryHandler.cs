using AutoMapper;
using Axess.Application.Models;
using Axess.Infrastructure.Contexts;
using Axess.MediatR.OData.Queries;
using MediatR;

namespace Axess.MediatR.Order.Queries.OData.V4;
using OrderDto = OrderDto;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderIQueryableQueryHandler : IRequestHandler<ODataOptionsIQueryableQuery<OrderDto>, IQueryable>
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
    public async Task<IQueryable> Handle(ODataOptionsIQueryableQuery<OrderDto> request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(request.Options.ApplyTo(_mapper.ProjectTo<OrderDto>(_dbContext.Orders)));
    }
}
