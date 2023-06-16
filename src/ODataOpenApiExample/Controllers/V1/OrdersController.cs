namespace Axess.Controllers.V1;

using ApiVersioning.Examples.Models;
using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Results;
using Microsoft.AspNetCore.OData.Routing.Controllers;
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
public class OrdersController : ODataController
{


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
        SingleResult.Create(new[] { new OrderDto() { Code = Guid.NewGuid(), Customer = "Bill Mei" } }.AsQueryable());

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
        SingleResult.Create(new[] { new OrderDto() { Code = key, Customer = "Bill Mei" } }.AsQueryable());

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
    public IActionResult GetLineItems(Guid key)
    {
        LineItemDto[] lineItems = new LineItemDto[]
        {
            new() { Code = Guid.NewGuid(), Quantity = 1, UnitPrice = 2m, Description = "Dry erase wipes" },
            new() { Code = Guid.NewGuid(), Quantity = 1, UnitPrice = 3.5m, Description = "Dry erase eraser" },
            new() { Code = Guid.NewGuid(), Quantity = 1, UnitPrice = 5m, Description = "Dry erase markers" },
        };

        return Ok(lineItems);
    }
}