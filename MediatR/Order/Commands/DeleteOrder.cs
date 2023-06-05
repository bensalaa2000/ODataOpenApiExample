namespace ODataOpenApiExample.MediatR.Order.Commands;
using global::MediatR;

public class DeleteOrder : IRequest
{
    public int Id { get; set; }
}
