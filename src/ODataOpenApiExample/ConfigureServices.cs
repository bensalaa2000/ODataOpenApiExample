using AutoMapper.Extensions.ExpressionMapping;
using Axess.Infrastructure;
using Axess.Mappings.Profiles;
using FluentValidation;
using MediatR;
//using MediatR;
using System.Reflection;
namespace Axess;

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


        //services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddExpressionMapping();
        }, typeof(Program).Assembly, typeof(MappingProfile).Assembly/*Assembly.GetExecutingAssembly()*/);

        //services.AddMediator(typeof(ConfigureServices).Assembly);

        /*services.TryAddScoped<ODataOpenApiExample.MediatR.IMediator, ODataOpenApiExample.MediatR.Mediator>();*/
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}
