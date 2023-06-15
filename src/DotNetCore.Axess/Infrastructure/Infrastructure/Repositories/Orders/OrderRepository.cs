using DotNetCore.Axess.Entities;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using Microsoft.Extensions.Logging;
using ODataMappingApi.Repositories.Orders;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    /// <inheritdoc/>
    public OrderRepository(ApplicationDbContext dbContext, ILogger<Order> logger) : base(dbContext, logger) { }

}
