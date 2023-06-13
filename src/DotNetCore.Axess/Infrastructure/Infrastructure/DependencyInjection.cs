using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Axess.Infrastructure;

public static class DependencyInjection
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration, bool useInMemoryDatabase)
    {
        if (useInMemoryDatabase/*configuration.GetValue<bool>("UseInMemoryDatabase")*/)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("TestDB");
                options.EnableSensitiveDataLogging();
            });

        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
        }
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDB"));
        // Ajoute l'interface du contexte aux services.
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<ApplicationDbContextInitialiser>();

        //services.AddTransient(typeof(IRepository<,>), typeof(Repository<,>));
    }
}
