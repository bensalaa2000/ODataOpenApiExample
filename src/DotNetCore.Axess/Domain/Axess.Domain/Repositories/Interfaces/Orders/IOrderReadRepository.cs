
using Axess.Common.Domain.Repositories.Interfaces;
using Entities = DotNetCore.Axess.Entities;
namespace ODataMappingApi.Repositories.Orders;

public interface IOrderReadRepository : IQueryRepository<Entities.Order> { }
