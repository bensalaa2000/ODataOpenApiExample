using Axess.Domain.Entities;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.Infrastructure.Contexts;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderCommandRepository : CommandRepository<Order>, IOrderCommandRepository
{
    /// <inheritdoc/>
    public OrderCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
