namespace ODataOpenApiExample.Controllers.V4;

using ApiVersioning.Examples.Models;
using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ODataOpenApiExample.MediatR.OData.Queries;
using System.Collections.Generic;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;
/// <summary>
/// Represents a RESTful service of orders.
/// </summary>
[ApiVersion(4.0)]
public class OrdersController : ApiODataControllerBase
{
    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>All available orders.</returns>
    /// <response code="200">Orders successfully retrieved.</response>
    /// <response code="400">The order is invalid.</response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ODataValue<IEnumerable<Order>>), Status200OK)]
    public async Task<IActionResult> Get(ODataQueryOptions<Order> options)
    {
        return Ok(Mediator.Send(new ODataOptionsQuery<Order>(options)));
    }

}