using AutoMapper.Extensions.ExpressionMapping;
using Axess.Common.Application.Behaviours;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Axess.Application;

public static class DependencyInjection
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddAutoMapper(cfg =>
		{
			cfg.AddExpressionMapping();
		}, Assembly.GetExecutingAssembly());

		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);
		//AddValidatorsFromAssemblyContaining
		services.AddFluentValidationAutoValidation(config =>
		{

		}).AddFluentValidationClientsideAdapters();
		/** DEPRECATED services.AddFluentValidation(config => {
			// Add all validators in the current assembly
			config.RegisterValidatorsFromAssemblyContaining<OrderValidator>();
			
			// Alternatively, you can add individual validators manually:
			// config.AddValidator<MyValidator>();
		});*/

		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));// --> Axess.Common.Application.Behaviours
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));// --> Axess.Common.Application.Behaviours
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));// --> Axess.Common.Application.Behaviours
		return services;
	}
}
