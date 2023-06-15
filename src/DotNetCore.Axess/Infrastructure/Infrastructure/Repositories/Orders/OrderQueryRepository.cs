using DotNetCore.Axess.Entities;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using ODataMappingApi.Repositories.Orders;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderQueryRepository : QueryRepository<Order>, IOrderQueryRepository
{
    /// <inheritdoc/>
    public OrderQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
