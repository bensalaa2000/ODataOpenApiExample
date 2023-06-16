//using AutoMapper;
using DotNetCore.Axess.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Axess.Infrastructure.Contexts;
using Axess.Shared;

namespace Axess.Application.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ApiResult<string>>
{
    private readonly IApplicationDbContext _context;
    //private readonly IMapper _mapper;

    private readonly ILogger<CreateOrderCommandHandler> _logger;

    public CreateOrderCommandHandler(IApplicationDbContext context/*, IMapper mapper,*/, ILogger<CreateOrderCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<ApiResult<string>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Order newOrder = new Order(Guid.NewGuid())
        {
            Customer = request.CustomerId
        };

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

        await _context.Orders.AddAsync(newOrder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new ApiResult<string>(true, $"Order with Id: {newOrder.Id} created successfully");
    }
}