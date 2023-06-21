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
public sealed class ODataOptionsOrdeSingleResultQueryHandler : IRequestHandler<ODataOptionsSingleResultQuery<OrderDto>, SingleResult<OrderDto>>
{
	//https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

	private readonly IOrderReadRepository orderReadRepository;

	private readonly IMapper _mapper;
	/// <inheritdoc/>
	public ODataOptionsOrdeSingleResultQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
	{
		this.orderReadRepository = orderReadRepository;
		_mapper = mapper;
	}

	/// <inheritdoc/>
	public async Task<SingleResult<OrderDto>> Handle(ODataOptionsSingleResultQuery<OrderDto> request, CancellationToken cancellationToken)
	{
		var entities = orderReadRepository.Queryable;
		var orders = entities.ProjectAndApplyToIQueryable(_mapper, request.options);
		var singleResult = SingleResult.Create(orders);
		return await Task.FromResult(singleResult);

	}
}
