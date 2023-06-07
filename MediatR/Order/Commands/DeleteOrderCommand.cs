namespace ODataOpenApiExample.MediatR.Order.Commands;
using global::MediatR;

public class DeleteOrderCommand : IRequest
{
    public int Id { get; set; }
}
