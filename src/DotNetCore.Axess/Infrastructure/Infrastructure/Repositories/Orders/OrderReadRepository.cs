using DotNetCore.Axess.Entities;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using ODataMappingApi.Repositories.Orders;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderReadRepository : QueryRepository<Order>, IOrderReadRepository
{
    /// <inheritdoc/>
    public OrderReadRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
