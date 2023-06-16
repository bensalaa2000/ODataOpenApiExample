using Axess.Common.Domain.Repositories;

namespace Axess.Domain.Repositories.Interfaces.Orders;

public interface IOrderCommandRepository : ICommandRepository<Entities.Order> { }