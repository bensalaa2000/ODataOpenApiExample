using DotNetCore.Axess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.Axess.Infrastructure.Persistence.Contexts;

public interface IApplicationDbContext
{
    /// <summary>
    /// 
    /// </summary>
    DbSet<Address> Addresses { get; }
    /// <summary>
    /// 
    /// </summary>
    DbSet<Order> Orders { get; }
    /// <summary>
    /// 
    /// </summary>
    DbSet<LineItem> LineItems { get; }
    /// <summary>
    /// 
    /// </summary>
    DbSet<Person> Persons { get; }
    /// <summary>
    /// 
    /// </summary>
    DbSet<Product> Products { get; }
    /// <summary>
    /// 
    /// </summary>
    DbSet<Supplier> Suppliers { get; }

    #region
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    #endregion
}
