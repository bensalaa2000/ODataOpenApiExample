using Axess.Common.Infrastructure.Repositories;
using Axess.Domain.Entities;
using Axess.Domain.Repositories.Interfaces.Orders;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderReadRepository : QueryRepository<Order>, IOrderReadRepository
{
    /// <inheritdoc/>
    public OrderReadRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
