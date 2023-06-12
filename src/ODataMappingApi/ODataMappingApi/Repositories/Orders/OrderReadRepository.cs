using DotNetCore.Axess.Entities;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using DotNetCore.Axess.Repositories;

namespace ODataMappingApi.Repositories.Orders;

public class OrderReadRepository : QueryRepository<Order, int>, IOrderReadRepository
{
    /// <inheritdoc/>
    public OrderReadRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
