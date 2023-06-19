namespace Axess.Application.MediatR.Orders.Commands;
using global::MediatR;

public class DeleteOrderCommand : IRequest
{
	public Guid Id { get; set; }
}
