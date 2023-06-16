using Axess.Domain.Entities;
using Axess.Domain.Repositories.Interfaces.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Axess.Domain.Evrp.Api.Controllers.Medtra.V1;

[ApiVersion("1.0")]
public class ModelsOrdersController : ControllerBase //ODataControllerBase<OrderDto>
{
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    private readonly IOrderQueryRepository _orderReadRepository;
    #region Constructeurs
    public ModelsOrdersController(IOrderQueryRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
    }
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
    public /*override async*/ IActionResult Get(ODataQueryOptions<Order> queryOptions, CancellationToken ct)
    {
        IQueryable finalQuery = queryOptions.ApplyTo(_orderReadRepository.Queryable);
        return Ok(finalQuery);
        //return Ok(Mediator.Send(new ODataOptionsQueryOrder(options)));
    }

    [HttpGet("odata/ModelsOrders/{id}")]
    public async Task<IActionResult> Get(Guid id, ODataQueryOptions<Order> queryOptions)
    {
        IQueryable<Order> accountQuery = _orderReadRepository.Queryable.Where(c => c.Id == id);
        if (!accountQuery.Any())
        {
            return NotFound();
        }

        IQueryable<dynamic>? finalQuery = queryOptions.ApplyTo(accountQuery.AsQueryable<Order>()) as IQueryable<dynamic>;
        dynamic result = await finalQuery.FirstOrDefaultAsync();

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
    #endregion
}