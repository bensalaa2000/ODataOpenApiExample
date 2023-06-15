using Axess.Domain.Repositories.Interfaces.Orders;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace ODataMappingApi.Controllers.V2;

[ApiVersion("2.0")]
public class OrdersController : ControllerBase
{
    #region Propriétés
    private ISender _mediator = null!;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    private readonly IOrderQueryRepository _orderReadRepository;

    #endregion
    /// <inheritdoc/>
    public OrdersController(IOrderQueryRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
    }
    #region Actions

    /// <summary>
    /// Gets documents to Medtra.
    /// </summary>
    /// <param name="options">The current OData query options.</param>
    /// <param name="ct"></param>
    /// <returns>The requested documents.</returns>
    /// <response code="200">Documents was successfully retrieved.</response>
    [HttpGet]
    [EnableQuery]
    [MapToApiVersion("2.0")]
    public IActionResult Get()
    {
        return Ok(_orderReadRepository.Queryable);
    }

    [HttpGet]
    [EnableQuery]
    [MapToApiVersion("2.0")]
    public async Task<IActionResult> Get([FromRoute] Guid key)
    {
        Axess.Domain.Entities.Order? c = await _orderReadRepository.GetByIdAsync(key);
        if (c is null)
        {
            return NotFound($"Cannot find customer with Id={key}");
        }

        return Ok(c);
    }
    #endregion
}