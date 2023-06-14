using AutoMapper;
using Axess.Infrastructure.Contexts;
using Axess.MediatR.OData.Queries;
using MediatR;
using System.Collections;

namespace Axess.MediatR.Order.Queries.OData.V4;
using OrderDto = ApiVersioning.Examples.Models.OrderDto;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderIEnumerableQueryHandler : IRequestHandler<ODataOptionsIEnumerableQuery<OrderDto>, IEnumerable>
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
    public async Task<IEnumerable> Handle(ODataOptionsIEnumerableQuery<OrderDto> request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(request.Options.ApplyTo(_mapper.ProjectTo<OrderDto>(_dbContext.Orders)) as IEnumerable<OrderDto>);
    }
}
