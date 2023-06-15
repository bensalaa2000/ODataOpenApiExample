using Axess.Common.Domain.Repositories.Interfaces;

namespace Axess.Domain.Repositories.Interfaces.Orders;

public interface IOrderCommandRepository : ICommandRepository<Entities.Order> { }
