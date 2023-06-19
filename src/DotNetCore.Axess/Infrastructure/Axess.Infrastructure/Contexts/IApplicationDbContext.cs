using Axess.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Axess.Infrastructure.Contexts;

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
	Task SaveChangesAsync(CancellationToken cancellationToken = default);

	#endregion
}
