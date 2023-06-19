namespace Axess.Application.MediatR.Orders.Commands.V3;

using AutoMapper;
using Axess.Application.Models;
using Axess.Domain.Entities;
using Axess.Infrastructure.Contexts;
using global::MediatR;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// 
/// </summary>
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, OrderDto>
{
	private readonly IApplicationDbContext _dbContext;

	private readonly IMapper _mapper;
	/// <summary>
	/// 
	/// </summary>
	/// <param name="context"></param>
	/// <param name="mapper"></param>
	public CreateOrderCommandHandler(IApplicationDbContext context, IMapper mapper)
	{
		_dbContext = context;
		_mapper = mapper;
	}

	public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
	{
		var order = new OrderDto
		{
			/*Age = request.Age,
            FirstName = request.FirstName*/
		};

		_dbContext.Orders.Add(_mapper.Map<Order>(order));
		await _dbContext.SaveChangesAsync();
		return order;
	}
}
