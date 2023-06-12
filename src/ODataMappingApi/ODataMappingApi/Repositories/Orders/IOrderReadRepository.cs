
using Axess.Repositories.Interfaces;
using Entities = Axess.Entities;
namespace ODataMappingApi.Repositories.Orders;

public interface IOrderReadRepository : IQueryRepository<Entities.Order, int> { }
