namespace Axess.Controllers.V2;

using Asp.Versioning;
using Asp.Versioning.OData;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Axess.Application.Models;
using Axess.Domain.Repositories.Orders;
using Axess.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;

/// <summary>
/// Represents a RESTful service of orders.
/// </summary>
[ApiVersion(2.0)]
public class OrdersController : ApiODataControllerBase
{
	private readonly IOrderReadRepository orderReadRepository;

	private readonly IMapper _mapper;

	public OrdersController(IOrderReadRepository orderReadRepository, IMapper mapper)
	{
		this.orderReadRepository = orderReadRepository;
		this._mapper = mapper;

	}
	/// <summary>
	/// Retrieves all orders.
	/// </summary>
	/// <returns>All available orders.</returns>
	/// <response code="200">The successfully retrieved orders.</response>
	[HttpGet]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(ODataValue<IEnumerable<OrderDto>>), Status200OK)]
	[EnableQuery(MaxTop = 100, AllowedQueryOptions = Select | Top | Skip)]
	public IQueryable<OrderDto> Get()
	{
		return orderReadRepository.Queryable.ProjectTo<OrderDto>(_mapper.ConfigurationProvider);
	}

	/// <summary>
	/// Gets a single order.
	/// </summary>
	/// <param name="key">The requested order identifier.</param>
	/// <returns>The requested order.</returns>
	/// <response code="200">The order was successfully retrieved.</response>
	/// <response code="404">The order does not exist.</response>
	[HttpGet]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(OrderDto), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	[EnableQuery(AllowedQueryOptions = Select)]
	public SingleResult<OrderDto> Get(Guid key) =>
		SingleResult.Create(orderReadRepository.Queryable.Where(x => x.Id == key).ProjectTo<OrderDto>(_mapper.ConfigurationProvider));

	/// <summary>
	/// Places a new order.
	/// </summary>
	/// <param name="order">The order to place.</param>
	/// <returns>The created order.</returns>
	/// <response code="201">The order was successfully placed.</response>
	/// <response code="400">The order is invalid.</response>
	[HttpPost]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(OrderDto), Status201Created)]
	[ProducesResponseType(Status400BadRequest)]
	public IActionResult Post([FromBody] OrderDto order)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		order.Code = Guid.NewGuid();

		return Created(order);
	}

	/// <summary>
	/// Updates an existing order.
	/// </summary>
	/// <param name="key">The requested order identifier.</param>
	/// <param name="delta">The partial order to update.</param>
	/// <returns>The created order.</returns>
	/// <response code="204">The order was successfully updated.</response>
	/// <response code="400">The order is invalid.</response>
	/// <response code="404">The order does not exist.</response>
	[HttpPatch]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(OrderDto), Status200OK)]
	[ProducesResponseType(Status204NoContent)]
	[ProducesResponseType(Status400BadRequest)]
	[ProducesResponseType(Status404NotFound)]
	public IActionResult Patch(Guid key, [FromBody] Delta<OrderDto> delta)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var order = new OrderDto() { Code = key, Customer = "Bill Mei" };

		delta.Patch(order);

		return Updated(order);
	}

	/// <summary>
	/// Gets the most expensive order.
	/// </summary>
	/// <returns>The most expensive order.</returns>
	/// <response code="200">The order was successfully retrieved.</response>
	/// <response code="404">The no orders exist.</response>
	[HttpGet]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(OrderDto), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	[EnableQuery(AllowedQueryOptions = Select)]
	public SingleResult<OrderDto> MostExpensive() =>
		SingleResult.Create(new[] { new OrderDto() { Code = Guid.NewGuid(), Customer = "Bill Mei" } }.AsQueryable());

	/// <summary>
	/// Rates an order.
	/// </summary>
	/// <param name="key">The requested order identifier.</param>
	/// <param name="parameters">The action parameters.</param>
	/// <returns>None</returns>
	/// <response code="204">The order was successfully rated.</response>
	/// <response code="400">The parameters are invalid.</response>
	/// <response code="404">The order does not exist.</response>
	[HttpPost]
	[ProducesResponseType(Status204NoContent)]
	[ProducesResponseType(Status400BadRequest)]
	[ProducesResponseType(Status404NotFound)]
	public IActionResult Rate(Guid key, [FromBody] ODataActionParameters parameters)
	{
		if (!ModelState.IsValid)
		{
			return BadRequest(ModelState);
		}

		var rating = (int)parameters["rating"];
		return NoContent();
	}

	/// <summary>
	/// Gets the line items for the specified order.
	/// </summary>
	/// <param name="key">The order identifier.</param>
	/// <returns>The order line items.</returns>
	/// <response code="200">The line items were successfully retrieved.</response>
	/// <response code="404">The order does not exist.</response>
	[HttpGet]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(ODataValue<IEnumerable<LineItemDto>>), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	[EnableQuery(AllowedQueryOptions = Select | Count)]
	public async Task<IActionResult> GetLineItems(Guid key)
	{
		return Ok(await orderReadRepository.Queryable
			.Where(x => x.Id == key)
			.SelectMany(x => x.LineItems)
			.ProjectTo<LineItemDto>(_mapper.ConfigurationProvider)
			.ToListAsync());
		//return Ok(await applicationDbContext.LineItems.Where(x => x.Order.Id == key).ProjectTo<LineItemDto>(_mapper.ConfigurationProvider).ToListAsync());
	}
}