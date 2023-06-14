//using MediatR;
using DotNetCore.Axess.Repositories;
using DotNetCore.Axess.Repositories.Interfaces;
using MediatR;
using ODataMappingApi.Repositories.Orders;
using Shared.Application;
using System.Reflection;
namespace Shared;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.AddPersistence(configuration, configuration.GetValue<bool>("UseInMemoryDatabase"));
        /*if (configuration.GetValue<bool>("UseInMemoryDatabase"))
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
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDB"));*/

        // Ajoute l'interface du contexte aux services.
        //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        //services.AddScoped(typeof(IApplicationDbContext), typeof(ApplicationDbContext));

        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        // Ajoute les repositories
        services.AddTransient<IOrderReadRepository, OrderReadRepository>();
        //services.AddScoped<ApplicationDbContextInitialiser>();


        /*A placer dan sla couche Application*/


        services.AddApplication();

        //services.AddMediator(typeof(ConfigureServices).Assembly);

        /*services.TryAddScoped<ODataOpenApiExample.MediatR.IMediator, ODataOpenApiExample.MediatR.Mediator>();*/
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}
