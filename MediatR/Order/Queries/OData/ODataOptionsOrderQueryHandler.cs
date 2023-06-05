using AutoMapper;
using MediatR;
using ODataOpenApiExample.MediatR.OData.Queries;
using ODataOpenApiExample.Persistence.Contexts;

namespace ODataOpenApiExample.MediatR.Order.Queries.OData;
using Order = ApiVersioning.Examples.Models.Order;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
public sealed class ODataOptionsOrderQueryHandler : IRequestHandler<ODataOptionsQuery<Order>, IQueryable>
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
{

    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public ODataOptionsOrderQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        this._dbContext = context;
        this._mapper = mapper;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<IQueryable> Handle(ODataOptionsQuery<Order> request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(request.options.ApplyTo(_mapper.ProjectTo<Order>(_dbContext.Orders)));
    }
}
