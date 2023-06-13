﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Shared.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        /*services.AddAutoMapper(cfg =>
        {
            cfg.AddExpressionMapping();
        }, Assembly.GetExecutingAssembly());*/
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}