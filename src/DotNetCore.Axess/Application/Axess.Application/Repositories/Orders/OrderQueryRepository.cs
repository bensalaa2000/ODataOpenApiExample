using Axess.Infrastructure.Repositories;
using DotNetCore.Axess.Entities;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;

namespace ODataMappingApi.Repositories.Orders;

public class OrderQueryRepository : QueryRepository<Order>, IOrderQueryRepository
{
    /// <inheritdoc/>
    public OrderQueryRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
