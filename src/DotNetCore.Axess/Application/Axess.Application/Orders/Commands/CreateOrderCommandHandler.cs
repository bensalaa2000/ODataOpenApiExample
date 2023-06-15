//using AutoMapper;
using AutoMapper;
using Axess.Shared;
using Axess.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Axess.Domain.Repositories.Interfaces.Orders;

namespace Axess.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<string>>
{
    private readonly IOrderCommandRepository _orderCommandRepository;
    private readonly IMapper _mapper;

    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IOrderCommandRepository orderCommandRepository, IMapper mapper, ILogger<CreateOrderCommandHandler> logger)
    {
        _orderCommandRepository = orderCommandRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ApiResult<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        //Order newOrder = _mapper.Map<Order>(request);

        Order newOrder = new Order(Guid.NewGuid(), request.CustomerId);

        List<LineItem> lineItems = request.OrderItemList.Select(item => new LineItem(Guid.NewGuid())
        {
            UnitPrice = item.Price,
            Quantity = item.Count,
        }).ToList();
        lineItems?.ForEach(item =>
        {
            newOrder.AddLineItem(new LineItem(Guid.NewGuid())
            {
                OrderId = newOrder.Id,
                Description = item.Description,
            });
        });

        await _orderCommandRepository.AddAsync(newOrder, cancellationToken);
        await _orderCommandRepository.SaveChangesAsync(cancellationToken);

        return new ApiResult<string>(true, $"Order with Id: {newOrder.Id} created successfully");
    }
}