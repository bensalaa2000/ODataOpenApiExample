using Axess.Domain.Entities;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.Infrastructure.Contexts;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderQueryRepository : QueryRepository<Order>, IOrderQueryRepository
{
    /// <inheritdoc/>
    public OrderQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
