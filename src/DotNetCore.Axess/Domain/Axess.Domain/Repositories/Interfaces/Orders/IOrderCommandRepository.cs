
using DotNetCore.Axess.Repositories.Interfaces;
using Entities = DotNetCore.Axess.Entities;
namespace ODataMappingApi.Repositories.Orders;

public interface IOrderCommandRepository : ICommandRepository<Entities.Order> { }
