using Axess.Common.Domain.Repositories.Interfaces;
using Axess.Common.Infrastructure.Repositories;
using Axess.Domain.Repositories.Interfaces.Orders;
using Axess.Infrastructure.Contexts;
using Axess.Infrastructure.Repositories.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Axess.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
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
        //services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDB"));
        // Ajoute l'interface du contexte aux services.
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        // Ajoute les repositories
        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IOrderQueryRepository, OrderQueryRepository>();
        services.AddTransient<IOrderCommandRepository, OrderCommandRepository>();

        return services;
    }
}
