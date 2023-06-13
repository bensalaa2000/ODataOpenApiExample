namespace Axess.Controllers.V5;

using Asp.Versioning;
using Asp.Versioning.OData;
using Axess.MediatR.OData.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Entities = DotNetCore.Axess.Entities;
/// <summary>
/// Represents a RESTful service of orders.
/// </summary>
[ApiVersion(5.0)]
public class OrdersController : ApiODataControllerBase
{
    /// <summary>
    /// Retrieves all orders from entities.
    /// </summary>
    /// <returns>All available orders.</returns>
    /// <response code="200">Orders successfully retrieved.</response>
    /// <response code="400">The order is invalid.</response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(ODataValue<IEnumerable<Entities.Order>>), Status200OK)]
    public IActionResult Get(ODataQueryOptions<Entities.Order> options) =>
        Ok(Mediator.Send(new ODataOptionsQuery<Entities.Order>(options)));



    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    [HttpGet]//("api/Orders/{key:int}") // ("api/Orders({key}}")
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(Entities.Order), Status200OK)]
    [ProducesResponseType(Status404NotFound)]
    //[ODataRouteComponent("({key})")]
    //[EnableQuery(AllowedQueryOptions = AllowedQueryOptions.Select)]
    public IActionResult/*async Task<SingleResult<Order>>*/ Get(Guid key, ODataQueryOptions<Entities.Order> options)
    {
        /*SingleResult<Order>*/
        //Task<SingleResult<Order>> result = Mediator.Send(new ODataOptionsByIdQuery<Order>(options, key));
        Task<IQueryable<Entities.Order>> result = Mediator.Send(new ODataOptionsByIdIQueryableQuery<Entities.Order>(options, key));
        return Ok(result);
    }
}