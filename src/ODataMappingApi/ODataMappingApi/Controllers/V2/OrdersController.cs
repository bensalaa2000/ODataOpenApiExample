using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ODataMappingApi.Repositories.Orders;

namespace ODataMappingApi.Controllers.V2;

[ApiVersion("2.0")]
public class OrdersController : ControllerBase
{

    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IApplicationDbContext _dbContext;
    /// <inheritdoc/>
    public OrdersController(IApplicationDbContext context, IOrderReadRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
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
        IQueryable<DotNetCore.Axess.Entities.Order> orders = _orderReadRepository.Queryable;
        //Microsoft.EntityFrameworkCore.DbSet<Axess.Entities.Order> orders = _dbContext.Orders;
        //int count = orders.Count();
        int count = _orderReadRepository.Queryable.Count();
        return Ok(orders);
    }

    [HttpGet]
    [EnableQuery]
    [MapToApiVersion("2.0")]
    public IActionResult Get([FromRoute] int key)
    {
        DotNetCore.Axess.Entities.Order? c = _dbContext.Orders.AsQueryable().SingleOrDefault(c => c.Id.Equals(key));
        if (c is null)
        {
            return NotFound($"Cannot find customer with Id={key}");
        }

        return Ok(c);
    }
    #endregion
}