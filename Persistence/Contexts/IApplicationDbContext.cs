using Microsoft.EntityFrameworkCore;
using ODataOpenApiExample.Persistence.Entities;

namespace ODataOpenApiExample.Persistence.Contexts;

public interface IApplicationDbContext
{
    DbSet<Address> Addresses { get; }
    DbSet<Order> Orders { get; }
    DbSet<LineItem> LineItems { get; }
    DbSet<Person> Persons { get; }
    DbSet<Product> Products { get; }
    DbSet<Supplier> Suppliers { get; }

    #region
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    #endregion
}
