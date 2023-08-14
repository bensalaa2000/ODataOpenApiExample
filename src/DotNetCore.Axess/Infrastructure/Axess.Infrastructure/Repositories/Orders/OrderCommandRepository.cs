using Axess.Common.Infrastructure.Repositories;
using Axess.Domain.Entities;
using Axess.Domain.Repositories.Orders;
using Axess.Infrastructure.Contexts;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderCommandRepository : CommandRepository<Order>, IOrderCommandRepository
{
    /// <inheritdoc/>
    public OrderCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
