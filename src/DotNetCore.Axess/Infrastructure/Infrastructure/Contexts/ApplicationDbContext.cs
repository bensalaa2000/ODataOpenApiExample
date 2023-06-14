using DotNetCore.Axess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DotNetCore.Axess.Infrastructure.Persistence.Contexts;
/// <summary>
/// 
/// </summary>
public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{

    protected readonly IConfiguration Configuration;

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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())/*.Seed()*/;
        base.OnModelCreating(builder);
    }

    async Task IApplicationDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }

    async Task IApplicationDbContext.SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken)
    {
        await SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<LineItem> LineItems => Set<LineItem>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
}

