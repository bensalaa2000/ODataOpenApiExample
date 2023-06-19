using Axess.Domain.Entities;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.MediatR.OData.Queries;
using MediatR;
using Microsoft.AspNetCore.OData.Results;

namespace Axess.Application.MediatR.Orders.Queries.V5;
/// <summary>
/// 
/// </summary>
public sealed class ODataOptionsOrderByIdQueryHandler : IRequestHandler<ODataOptionsByIdQuery<Order>, SingleResult<Order>>
{
	//https://csharp.hotexamples.com/examples/-/ODataQueryOptions/ApplyTo/php-odataqueryoptions-applyto-method-examples.html

	private readonly IOrderReadRepository orderReadRepository;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	/// <param name="mapper"></param>
	public ODataOptionsOrderByIdQueryHandler(IOrderReadRepository orderReadRepository)
	{
		this.orderReadRepository = orderReadRepository;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="request"></param>
	/// <param name="cancellationToken"></param>
	/// <returns></returns>
	public async Task<SingleResult<Order>> Handle(ODataOptionsByIdQuery<Order> request, CancellationToken cancellationToken)
	{
		////IQueryable<Order> result = orderReadRepository.Queryable.Where(o => o.Id == request.Key);
		var orders = request.Options.ApplyTo(orderReadRepository.Queryable) as IQueryable<Order>;
		var singleResult = SingleResult.Create(orders);
		var resultat = await Task.FromResult(singleResult);
		return resultat;
	}
}