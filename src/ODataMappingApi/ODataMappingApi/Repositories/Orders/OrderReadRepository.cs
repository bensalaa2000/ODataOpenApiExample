using Axess.Entities;
using Axess.Infrastructure.Persistence.Contexts;
using Axess.Repositories;

namespace ODataMappingApi.Repositories.Orders;

public class OrderReadRepository : QueryRepository<Order, int>, IOrderReadRepository
{
    /// <inheritdoc/>
    public OrderReadRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
