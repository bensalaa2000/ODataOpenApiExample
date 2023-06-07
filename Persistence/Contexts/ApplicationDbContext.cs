using Microsoft.EntityFrameworkCore;
using ODataOpenApiExample.Persistence.Entities;
using System.Reflection;

namespace ODataOpenApiExample.Persistence.Contexts;
/// <summary>
/// 
/// </summary>
public sealed class ApplicationDbContext : DbContext, IApplicationDbContext
{
    //private readonly IMediator _mediator;
    protected readonly IConfiguration Configuration;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="configuration"></param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IConfiguration configuration/*,
         IMediator mediator*/) : base(options)
    {
        //_mediator = mediator;
        Configuration = configuration;
    }

    /* protected override void OnConfiguring(DbContextOptionsBuilder options)
     {
         // in memory database used for simplicity, change to a real db for production applications
         //options.UseInMemoryDatabase("TestDb");
     }*/

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())/*.Seed()*/;
        base.OnModelCreating(builder);
    }


    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<LineItem> LineItems => Set<LineItem>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Supplier> Suppliers => Set<Supplier>();
}

