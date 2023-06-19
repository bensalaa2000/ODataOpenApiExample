using AutoMapper;
using Axess.Application.Models;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.Extensions;
using Axess.MediatR.OData.Queries;
using MediatR;

namespace Axess.Application.MediatR.Orders.Queries.V4;
using OrderDto = OrderDto;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdIQueryableQueryHandler : IRequestHandler<ODataOptionsByIdIQueryableQuery<OrderDto>, IQueryable<OrderDto>>
{
	//https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

	private readonly IOrderReadRepository orderReadRepository;

	private readonly IMapper _mapper;
	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	/// <param name="mapper"></param>
	public ODataOptionsOrderByIdIQueryableQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
	{
		this.orderReadRepository = orderReadRepository;
		_mapper = mapper;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<IQueryable<OrderDto>> Handle(ODataOptionsByIdIQueryableQuery<OrderDto> request, CancellationToken cancellationToken)
	{
		var result = orderReadRepository.Queryable.Where(o => o.Id == request.Key);
		var orders = result.ProjectAndApplyToIQueryable(_mapper, request.Options);
		return await Task.FromResult(orders);
	}
}