using AutoMapper;
using Axess.Application.MediatR.OData.Queries;
using Axess.Application.Models;
using Axess.Domain.Repositories.Orders;
using Axess.Extensions;
using MediatR;
using Microsoft.AspNetCore.OData.Results;

namespace Axess.Application.MediatR.Orders.Queries.V4;
using OrderDto = OrderDto;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdQueryHandler : IRequestHandler<ODataOptionsByIdQuery<OrderDto>, SingleResult<OrderDto>>
{
	//https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

	private readonly IOrderReadRepository orderReadRepository;

	private readonly IMapper _mapper;
	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	/// <param name="mapper"></param>
	public ODataOptionsOrderByIdQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
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
	public async Task<SingleResult<OrderDto>> Handle(ODataOptionsByIdQuery<OrderDto> request, CancellationToken cancellationToken)
	{
		var result = orderReadRepository.Queryable.Where(o => o.Id == request.Key);
		var orders = result.ProjectAndApplyToIQueryable(_mapper, request.Options);
		var singleResult = SingleResult.Create(orders);
		var resultat = await Task.FromResult(singleResult);
		return resultat;
	}
}