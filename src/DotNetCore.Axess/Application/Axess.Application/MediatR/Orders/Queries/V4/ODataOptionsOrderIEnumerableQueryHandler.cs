using AutoMapper;
using Axess.Application.Models;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.MediatR.OData.Queries;
using MediatR;
using System.Collections;

namespace Axess.Application.MediatR.Orders.Queries.V4;
using OrderDto = OrderDto;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderIEnumerableQueryHandler : IRequestHandler<ODataOptionsIEnumerableQuery<OrderDto>, IEnumerable>
{
	//https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

	private readonly IOrderReadRepository orderReadRepository;

	private readonly IMapper _mapper;
	/// <inheritdoc/>
	public ODataOptionsOrderIEnumerableQueryHandler(IOrderReadRepository orderReadRepository, IMapper mapper)
	{
		this.orderReadRepository = orderReadRepository;
		_mapper = mapper;
	}

	/// <inheritdoc/>
	public async Task<IEnumerable> Handle(ODataOptionsIEnumerableQuery<OrderDto> request, CancellationToken cancellationToken)
	{
		var resultMapper = _mapper.ProjectTo<OrderDto>(orderReadRepository.Queryable);
		var result = (IEnumerable<OrderDto>)request.Options.ApplyTo(resultMapper)!;
		return await Task.FromResult(result);
	}
}
