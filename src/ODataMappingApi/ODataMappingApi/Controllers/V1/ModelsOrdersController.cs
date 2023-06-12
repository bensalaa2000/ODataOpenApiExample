﻿using Axess.Architecture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ODataMappingApi.MediatR.Queries;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace DotNetCore.Axess.Evrp.Api.Controllers.Medtra.V1;

[ApiVersion("1.0")]
public class ModelsOrdersController : ODataControllerBase<Order>
{

    #region Constructeurs
    /// <summary>
    /// Constucteur
    /// </summary>
    /// <param name="service">Service</param>
    /// <param name="logger">Logueur</param>
    //public OrdersController(/*IODataService<Order> service,*/
    //    ILogger<OrdersController> logger) : base(logger/*, service*/) { }
    #endregion

    #region Actions

    /// <summary>
    /// Gets documents to Medtra.
    /// </summary>
    /// <param name="options">The current OData query options.</param>
    /// <param name="ct"></param>
    /// <returns>The requested documents.</returns>
    /// <response code="200">Documents was successfully retrieved.</response>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<Order>), Status200OK)]
    [MapToApiVersion("1.0")]
    public override async Task<IActionResult> Get(ODataQueryOptions<Order> options, CancellationToken ct)
    {
        return Ok(Mediator.Send(new ODataOptionsQueryOrder(options)));
    }

    #endregion
}