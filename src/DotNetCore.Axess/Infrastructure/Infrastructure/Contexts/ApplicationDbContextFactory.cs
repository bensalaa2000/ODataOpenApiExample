using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ODataOpenApiExample.Persistence.Contexts;
/// <summary>
/// 
/// </summary>
public sealed class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{

    //protected readonly IMediator _mediator;
    protected readonly IConfiguration Configuration;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    public ApplicationDbContextFactory(/*IMediator mediator, */IConfiguration configuration)
    {
        // _mediator = mediator;
        Configuration = configuration;
    }

    /// <inheritdoc/>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        const string connectionString = "Host=localhost;Port=5432;Database=ContextFactory;Username=admin;Password=Medtr@26;SSL Mode=Prefer;Trust Server Certificate=true;Pooling=true;";
        return new ApplicationDbContext(/*_mediator,*/new DbContextOptionsBuilder<ApplicationDbContext>().UseNpgsql(connectionString).Options, Configuration);
    }
}