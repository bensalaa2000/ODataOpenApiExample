using Axess.Infrastructure.Repositories;
using DotNetCore.Axess.Entities;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;

namespace ODataMappingApi.Repositories.Orders;

public class OrderReadRepository : QueryRepository<Order>, IOrderReadRepository
{
    /// <inheritdoc/>
    public OrderReadRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}
