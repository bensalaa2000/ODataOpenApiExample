using Axess.Shared;
using MediatR;

namespace Axess.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<ApiResult<string>>
{
	public CreateOrderCommand()
	{
		OrderItemList = new List<OrderItemDto>();
	}
	public string CustomerId { get; set; } = string.Empty;
	public string PaymentAccountId { get; set; } = string.Empty;
	public List<OrderItemDto>? OrderItemList { get; set; }
}

public class OrderItemDto
{
	public int ProductId { get; set; }
	public int Count { get; set; }
	public decimal Price { get; set; }
}