using Axess.Api.Configuration;
using Axess.Application.Configuration;
using FluentValidation.AspNetCore;
///using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ODataMappingApi;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {

        services.AddControllers()/*.AddNewtonsoftJson()*/.AddOData(options =>
        {
            DefaultODataBatchHandler defaultBatchHandler = new DefaultODataBatchHandler();
            defaultBatchHandler.MessageQuotas.MaxNestingDepth = 2;
            defaultBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;
            defaultBatchHandler.MessageQuotas.MaxReceivedMessageSize = 100;

            options./*EnableQueryFeatures(50000).*/Select().Expand().Filter().Count().OrderBy().SetMaxTop(5000);
            options.AddRouteComponents("odata", EdmModelBuilder.GetEdmModel(), defaultBatchHandler
                /*services => services.AddSingleton<ODataBatchHandler, DefaultODataBatchHandler>()*/)
                    .Conventions.Add(new AxessODataConvention());
            options.TimeZone = TimeZoneInfo.Utc;

            options.RouteOptions.EnableKeyInParenthesis = false;
            options.RouteOptions.EnableNonParenthesisForEmptyParameterFunction = true;
            options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
            options.RouteOptions.EnableQualifiedOperationCall = false;
            options.RouteOptions.EnableUnqualifiedOperationCall = true;
        })
                    .AddJsonOptions(options =>
                    {
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        // serialize DateOnly as strings

                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                        options.JsonSerializerOptions.WriteIndented = true;
                    });

        // Add FluentValidation to Asp.net
        /***builder.Services.AddFluentValidationAutoValidation(config =>
        {
            ////config.DisableDataAnnotationsValidation = true;
        }).AddFluentValidationClientsideAdapters();*/

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        ///builder.Services.AddFluentValidationRulesToSwagger();
        services.AddSwaggerGen(c =>
        {
            c.ResolveConflictingActions(apiDescriptions =>
            {
                ApiDescription[] descriptions = apiDescriptions as ApiDescription[] ?? apiDescriptions.ToArray();
                ApiDescription first = descriptions[0]; // build relative to the 1st method
                first.ParameterDescriptions.Clear();
                List<ApiParameterDescription> parameters = descriptions
                .SelectMany(d => d.ParameterDescriptions)
                .Where(y => first.ParameterDescriptions.All(x => x.Name != y.Name)).ToList();

                // add parameters and make them optional
                foreach (ApiParameterDescription? parameter in parameters)
                    first.ParameterDescriptions.Add(new ApiParameterDescription
                    {
                        ModelMetadata = parameter.ModelMetadata,
                        Name = parameter.Name,
                        ParameterDescriptor = parameter.ParameterDescriptor,
                        Source = parameter.Source,
                        IsRequired = false,
                        DefaultValue = null
                    });

                return first;
            });
        });

        // Add FV
        services.AddFluentValidationAutoValidation();
        services.AddFluentValidationClientsideAdapters();
        // Add FV Rules to swagger
        ///services.AddFluentValidationRulesToSwagger();
        return services;
    }

}
