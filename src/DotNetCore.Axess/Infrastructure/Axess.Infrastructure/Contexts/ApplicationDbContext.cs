using Axess.Application.Common.Interfaces;
using Axess.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DotNetCore.Axess.Infrastructure.Persistence.Contexts;
/// <summary>
/// 
/// </summary>
public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{

	private readonly IConfiguration Configuration;

	/// <summary>
	/// 
	/// </summary>
	/// <param name="options"></param>
	/// <param name="configuration"></param>
	public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
		IConfiguration configuration) : base(options)
	{
		Configuration = configuration;
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())/*.Seed()*/;
		base.OnModelCreating(modelBuilder);
	}

	async Task IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
	{
		await SaveChangesAsync(cancellationToken);
	}

	public DbSet<Address> Addresses => Set<Address>();
	public DbSet<Order> Orders => Set<Order>();
	public DbSet<LineItem> LineItems => Set<LineItem>();
	public DbSet<Person> Persons => Set<Person>();
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Supplier> Suppliers => Set<Supplier>();
}

