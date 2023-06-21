using Axess.Common.Domain.Repositories;

namespace Axess.Domain.Repositories.Orders;

public interface IOrderCommandRepository : ICommandRepository<Entities.Order> { }