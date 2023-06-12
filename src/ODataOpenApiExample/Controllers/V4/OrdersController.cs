namespace Axess.Controllers.V4;

using Axess.Architecture.Models;
using Asp.Versioning;
using Asp.Versioning.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Axess.MediatR.OData.Queries;
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
    //[ProducesResponseType(typeof(PageResult<Order>), Status200OK)]
    public IActionResult Get(ODataQueryOptions<Order> options) =>
        Ok(Mediator.Send(new ODataOptionsQuery<Order>(options)));


    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>All available orders.</returns>
    /// <response code="200">Orders successfully retrieved.</response>
    /// <response code="400">The order is invalid.</response>
    [HttpGet("GetIEnumerable")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ODataValue<IEnumerable<Order>>), Status200OK)]
    public IActionResult GetIEnumerable(ODataQueryOptions<Order> options) =>
        Ok(Mediator.Send(new ODataOptionsIEnumerableQuery<Order>(options)));

    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>All available orders.</returns>
    /// <response code="200">Orders successfully retrieved.</response>
    /// <response code="400">The order is invalid.</response>
    [HttpGet("GetIQueryable")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ODataValue<IEnumerable<Order>>), Status200OK)]
    public IActionResult GetIQueryable(ODataQueryOptions<Order> options) =>
        Ok(Mediator.Send(new ODataOptionsIQueryableQuery<Order>(options)));

    /// <summary>
    /// Retrieves all orders.
    /// </summary>
    /// <returns>All available orders.</returns>
    /// <response code="200">Orders successfully retrieved.</response>
    /// <response code="400">The order is invalid.</response>
    [HttpGet("GetSingleResult")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ODataValue<IEnumerable<Order>>), Status200OK)]
    //[ProducesResponseType(typeof(PageResult<Order>), Status200OK)]
    public IActionResult GetSingleResult(ODataQueryOptions<Order> options) =>
        Ok(Mediator.Send(new ODataOptionsSingleResultQuery<Order>(options)));

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [HttpGet]//("api/Orders/{key:int}") // ("api/Orders({key}}")
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Order), Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    //[ODataRouteComponent("({key})")]
    //[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Select)]
    public IActionResult/*async Task<SingleResult<Order>>*/ Get(int key, ODataQueryOptions<Order> options)
    {
        /*SingleResult<Order>*/
        //Task<SingleResult<Order>> result = Mediator.Send(new ODataOptionsByIdQuery<Order>(options, key));
        Task<IQueryable<Order>> result = Mediator.Send(new ODataOptionsByIdIQueryableQuery<Order>(options, key));
        return Ok(result);
    }
}