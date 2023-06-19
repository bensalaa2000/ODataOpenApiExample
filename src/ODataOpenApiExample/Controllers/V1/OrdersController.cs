namespace Axess.Controllers.V1;

using Asp.Versioning;
using Asp.Versioning.OData;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Axess.Application.Models;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;

/// <summary>
/// Represents a RESTful service of orders.
/// </summary>
[ApiVersion(1.0)]
[ApiVersion(0.9, Deprecated = true)]
public class OrdersController : ApiODataControllerBase
{

	private readonly IOrderReadRepository orderReadRepository;

	private readonly IApplicationDbContext applicationDbContext;

	private readonly IMapper _mapper;

	public OrdersController(IOrderReadRepository orderReadRepository, IApplicationDbContext applicationDbContext, IMapper mapper)
	{
		this.orderReadRepository = orderReadRepository;
		this.applicationDbContext = applicationDbContext;
		this._mapper = mapper;

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
		SingleResult.Create(new[] { new OrderDto() { Code = Guid.NewGuid(), Customer = "John Doe" } }.AsQueryable());

	/// <summary>
	/// Places a new order.
	/// </summary>
	/// <param name="order">The order to place.</param>
	/// <returns>The created order.</returns>
	/// <response code="201">The order was successfully placed.</response>
	/// <response code="400">The order is invalid.</response>
	[HttpPost]
	[MapToApiVersion(1.0)]
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
	/// Gets the most expensive order.
	/// </summary>
	/// <returns>The most expensive order.</returns>
	/// <response code="200">The order was successfully retrieved.</response>
	/// <response code="404">The no orders exist.</response>
	[HttpGet]
	[MapToApiVersion("1.0")]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(OrderDto), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	[EnableQuery(AllowedQueryOptions = Select)]
	public SingleResult<OrderDto> MostExpensive() =>
		SingleResult.Create(orderReadRepository.Queryable.ProjectTo<OrderDto>(_mapper.ConfigurationProvider));

	/// <summary>
	/// Gets the most expensive order.
	/// </summary>
	/// <param name="key">The order identifier.</param>
	/// <returns>The most expensive order.</returns>
	/// <response code="200">The order was successfully retrieved.</response>
	/// <response code="404">The no orders exist.</response>
	[HttpGet]
	[MapToApiVersion(1.0)]
	[Produces(MediaTypeNames.Application.Json)]
	[ProducesResponseType(typeof(OrderDto), Status200OK)]
	[ProducesResponseType(Status404NotFound)]
	[EnableQuery(AllowedQueryOptions = Select)]
	public SingleResult<OrderDto> MostExpensive(Guid key) =>
		SingleResult.Create(orderReadRepository.Queryable.Where(x => x.Id == key).ProjectTo<OrderDto>(_mapper.ConfigurationProvider));

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
		return Ok(await applicationDbContext.LineItems.Where(x => x.Order.Id == key).ProjectTo<LineItemDto>(_mapper.ConfigurationProvider).ToListAsync());
	}
}