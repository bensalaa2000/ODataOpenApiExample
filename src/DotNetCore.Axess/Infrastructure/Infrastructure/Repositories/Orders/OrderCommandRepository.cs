using DotNetCore.Axess.Entities;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using ODataMappingApi.Repositories.Orders;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderCommandRepository : CommandRepository<Order>, IOrderCommandRepository
{
    /// <inheritdoc/>
    public OrderCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
