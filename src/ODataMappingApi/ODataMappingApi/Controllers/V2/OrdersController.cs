using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ODataOpenApiExample.Persistence.Contexts;

namespace ODataMappingApi.Controllers.V2;

[ApiVersion("2.0")]
public class OrdersController : ControllerBase
{


    private readonly IApplicationDbContext _dbContext;
    /// <inheritdoc/>
    public OrdersController(IApplicationDbContext context)
    {
        _dbContext = context;
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
        Microsoft.EntityFrameworkCore.DbSet<ODataOpenApiExample.Persistence.Entities.Order> orders = _dbContext.Orders;
        int count = orders.Count();
        return Ok(orders);
    }

    [HttpGet]
    [EnableQuery]
    [MapToApiVersion("2.0")]
    public IActionResult Get(int key)
    {
        ODataOpenApiExample.Persistence.Entities.Order? c = _dbContext.Orders.FirstOrDefault(c => c.Id == key);
        if (c is null)
        {
            return NotFound($"Cannot find customer with Id={key}");
        }

        return Ok(c);
    }
    #endregion
}