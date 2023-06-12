
using DotNetCore.Axess.Repositories.Interfaces;
using Entities = DotNetCore.Axess.Entities;
namespace ODataMappingApi.Repositories.Orders;

public interface IOrderReadRepository : IQueryRepository<Entities.Order, int> { }
