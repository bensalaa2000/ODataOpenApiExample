
using ApiVersioning.Examples;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using Axess.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;
using PeopleControllerV2 = Axess.Controllers.V2.PeopleController;
using PeopleControllerV3 = Axess.Controllers.V3.PeopleController;


namespace Axess;

public static class ConfigureServices
{
    public static IServiceCollection AddWebUIServices(this IServiceCollection services)
    {
        // IMvcBuilder
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                // ignore omitted parameters on models to enable optional params (e.g. User update)
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;

                //options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
            })
            .AddOData(
                options =>
                {
                    options.EnableQueryFeatures(50000);
                    //options.Count().Select().OrderBy();
                    options.RouteOptions.EnableKeyInParenthesis = true;
                    options.RouteOptions.EnableNonParenthesisForEmptyParameterFunction = true;
                    //options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
                    options.RouteOptions.EnableQualifiedOperationCall = true;
                    options.RouteOptions.EnableUnqualifiedOperationCall = true;
                })/*.AddFluentValidation(x => x.AutomaticValidationEnabled = false)*/;
        // IApiVersioningBuilder :  OData Versioning
        services.AddApiVersioning(
                            options =>
                            {
                                // reporting api versions will return the headers
                                // "api-supported-versions" and "api-deprecated-versions"
                                options.ReportApiVersions = true;

                                options.Policies.Sunset(0.9)
                                                .Effective(DateTimeOffset.Now.AddDays(60))
                                                .Link("policy.html")
                                                    .Title("Versioning Policy")
                                                    .Type("text/html");
                            })
                        .AddOData(options =>
                        {
                            DefaultODataBatchHandler defaultBatchHandler = new();
                            defaultBatchHandler.MessageQuotas.MaxNestingDepth = 2;
                            defaultBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;
                            defaultBatchHandler.MessageQuotas.MaxReceivedMessageSize = 100;

                            options.AddRouteComponents("api", defaultBatchHandler);
                        })
                        .AddODataApiExplorer(
                            options =>
                            {
                                // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                                // note: the specified format code will format the version as "'v'major[.minor][-status]"
                                options.GroupNameFormat = "'v'VVV";

                                // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                                // can also be used to control the format of the API version in route templates
                                options.SubstituteApiVersionInUrl = true;

                                // configure query options (which cannot otherwise be configured by OData conventions)
                                options.QueryOptions.Controller<PeopleControllerV2>()
                                                    .Action(c => c.Get(default))
                                                        .Allow(Skip | Count | Filter)
                                                        .AllowTop(100)
                                                        .AllowOrderBy("firstName", "lastName");

                                options.QueryOptions.Controller<PeopleControllerV3>()
                                                    .Action(c => c.Get(default))
                                                        .Allow(Skip | Count)
                                                        .AllowTop(100)
                                                        .AllowOrderBy("firstName", "lastName");
                            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddSwaggerGen(
            options =>
            {
                // add a custom operation filter which sets default values
                options.OperationFilter<SwaggerDefaultValues>();

                string fileName = typeof(Program).Assembly.GetName().Name + ".xml";
                string filePath = Path.Combine(AppContext.BaseDirectory, fileName);

                // integrate xml comments
                options.IncludeXmlComments(filePath);
            });

        return services;
    }
}
