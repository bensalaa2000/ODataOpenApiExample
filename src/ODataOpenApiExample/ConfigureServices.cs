using AutoMapper.Extensions.ExpressionMapping;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using FluentValidation;
using MediatR;
//using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Axess;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.

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
        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("TestDB"));

        // Ajoute l'interface du contexte aux services.
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        //services.AddScoped(typeof(IApplicationDbContext), typeof(ApplicationDbContext));


        services.AddScoped<ApplicationDbContextInitialiser>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddExpressionMapping();
        }, Assembly.GetExecutingAssembly());

        //services.AddMediator(typeof(ConfigureServices).Assembly);

        /*services.TryAddScoped<ODataOpenApiExample.MediatR.IMediator, ODataOpenApiExample.MediatR.Mediator>();*/
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}
