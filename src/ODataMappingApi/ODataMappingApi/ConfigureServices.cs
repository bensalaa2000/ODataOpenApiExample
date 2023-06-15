using Axess.Application.Configuration;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;

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

            options.EnableQueryFeatures(50000);//.Select().Filter().OrderBy().SetMaxTop(5000).Count().Expand()
            options.AddRouteComponents("odata", ODataConfiguration.GetEdmModel(), defaultBatchHandler);
            options.TimeZone = TimeZoneInfo.Utc;

            options.RouteOptions.EnableKeyInParenthesis = false;
            options.RouteOptions.EnableNonParenthesisForEmptyParameterFunction = true;
            options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
            options.RouteOptions.EnableQualifiedOperationCall = false;
            options.RouteOptions.EnableUnqualifiedOperationCall = true;
        })
                    /*.AddJsonOptions(options =>
                    {
                        ////options.JsonSerializerOptions.PropertyNamingPolicy = null;
                        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                        // serialize DateOnly as strings
                        //options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                        //options.JsonSerializerOptions.Converters.Add(new NullableDateOnlyJsonConverter());

                        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

                        options.JsonSerializerOptions.WriteIndented = true;
                    })*/;

        // Add FluentValidation to Asp.net
        /*builder.Services.AddFluentValidationAutoValidation(config =>
        {
            ////config.DisableDataAnnotationsValidation = true;
        }).AddFluentValidationClientsideAdapters();*/

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        //builder.Services.AddFluentValidationRulesToSwagger();
        services.AddSwaggerGen(c =>
        {
            c.ResolveConflictingActions(apiDescriptions =>
            {
                ApiDescription[] descriptions = apiDescriptions as ApiDescription[] ?? apiDescriptions.ToArray();
                ApiDescription first = descriptions.First(); // build relative to the 1st method
                List<ApiParameterDescription> parameters = descriptions.SelectMany(d => d.ParameterDescriptions).ToList();

                first.ParameterDescriptions.Clear();
                // add parameters and make them optional
                foreach (ApiParameterDescription? parameter in parameters)
                    if (first.ParameterDescriptions.All(x => x.Name != parameter.Name))
                    {
                        first.ParameterDescriptions.Add(new ApiParameterDescription
                        {
                            ModelMetadata = parameter.ModelMetadata,
                            Name = parameter.Name,
                            ParameterDescriptor = parameter.ParameterDescriptor,
                            Source = parameter.Source,
                            IsRequired = false,
                            DefaultValue = null
                        });
                    }
                return first;
            });
        });
        return services;
    }

}
