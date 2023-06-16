namespace Axess.Application.MediatR.Orders.Commands;

using AutoMapper;
using Axess.Application.Models;
using Axess.Domain.Entities;
using Axess.Infrastructure.Contexts;
using global::MediatR;
using Microsoft.EntityFrameworkCore;
public class OrderCommandsHandler
    : IRequestHandler<CreateOrderCommand, OrderDto>
    , IRequestHandler<UpdateOrderCommand, OrderDto>
    , IRequestHandler<DeleteOrderCommand>
{
    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public OrderCommandsHandler(IApplicationDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }


    public async Task<OrderDto> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        OrderDto order = new OrderDto
        {
            /*Age = request.Age,
            FirstName = request.FirstName*/
        };

        _dbContext.Orders.Add(_mapper.Map<Order>(order));
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<OrderDto> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        Order order = await _dbContext.Orders.SingleOrDefaultAsync(v => v.Id == request.Id);
        if (order == null)
        {
            // instead of throwing an exception here, we ideally indicate to the
            // client that he is sending a bad request. I will tackle this in an
            // upcoming post
            throw new Exception("Record does not exist");
        }
        /*person.Age = request.Age;
        person.FirstName = request.FirstName;*/
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<OrderDto>(order);
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        Order person = await _dbContext.Orders.SingleOrDefaultAsync(v => v.Id == request.Id);
        if (person == null)
        {
            throw new Exception("Record does not exist");
        }
        _dbContext.Orders.Remove(person);
        await _dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}