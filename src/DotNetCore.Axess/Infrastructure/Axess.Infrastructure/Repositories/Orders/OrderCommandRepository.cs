using Axess.Common.Infrastructure.Repositories;
using Axess.Domain.Entities;
using Axess.Domain.Repositories.Interfaces.Orders;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;

namespace Axess.Infrastructure.Repositories.Orders;

public class OrderCommandRepository : CommandRepository<Order>, IOrderCommandRepository
{
	/// <inheritdoc/>
	public OrderCommandRepository(ApplicationDbContext dbContext) : base(dbContext) { }

}