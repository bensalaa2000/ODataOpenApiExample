﻿namespace ODataOpenApiExample.MediatR.Order.Commands;

using ApiVersioning.Examples.Models;
using AutoMapper;
using global::MediatR;
using Microsoft.EntityFrameworkCore;
using ODataOpenApiExample.Persistence.Contexts;

public class OrderHandler
    : IRequestHandler<CreateOrder, Order>
    , IRequestHandler<UpdateOrder, Order>
    , IRequestHandler<DeleteOrder>
{
    private readonly IApplicationDbContext _dbContext;

    private readonly IMapper _mapper;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public OrderHandler(IApplicationDbContext context, IMapper mapper)
    {
        _dbContext = context;
        _mapper = mapper;
    }


    public async Task<Order> Handle(CreateOrder request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            /*Age = request.Age,
            FirstName = request.FirstName*/
        };

        _dbContext.Orders.Add(_mapper.Map<Persistence.Entities.Order>(order));
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<Order> Handle(UpdateOrder request, CancellationToken cancellationToken)
    {
        var order = await _dbContext.Orders.SingleOrDefaultAsync(v => v.Id == request.Id);
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
        return _mapper.Map<Order>(order);
    }

    public async Task<Unit> Handle(DeleteOrder request, CancellationToken cancellationToken)
    {
        var person = await _dbContext.Orders.SingleOrDefaultAsync(v => v.Id == request.Id);
        if (person == null)
        {
            throw new Exception("Record does not exist");
        }
        _dbContext.Orders.Remove(person);
        await _dbContext.SaveChangesAsync();
        return Unit.Value;
    }
}