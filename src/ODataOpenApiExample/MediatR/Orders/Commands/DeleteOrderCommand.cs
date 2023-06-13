namespace Shared.MediatR.Order.Commands;
using global::MediatR;

public class DeleteOrderCommand : IRequest
{
    public Guid Id { get; set; }
}
