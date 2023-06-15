using Axess.Infrastructure.Repositories;
using DotNetCore.Axess.Entities;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;

namespace ODataMappingApi.Repositories.Orders;

public class OrderCommanRepository : CommandRepository<Order>, IOrderCommandRepository
{
    /// <inheritdoc/>
    public OrderCommanRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
