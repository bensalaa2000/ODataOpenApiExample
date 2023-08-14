using Axess.Common.Infrastructure.Repositories;
using Axess.Domain.Entities;
using Axess.Domain.Repositories.Orders;
using Axess.Infrastructure.Contexts;
using Microsoft.Extensions.Logging;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderRepository : Repository<Order>, IOrderRepository
{
	/// <inheritdoc/>
	public OrderRepository(ApplicationDbContext dbContext, ILogger<Order> logger) : base(dbContext, logger) { }

}
