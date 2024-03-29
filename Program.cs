﻿using ApiVersioning.Examples;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;
using ODataOpenApiExample;
using ODataOpenApiExample.Filters;
using ODataOpenApiExample.Persistence.Contexts;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json.Serialization;
using static Microsoft.AspNetCore.OData.Query.AllowedQueryOptions;
using PeopleControllerV2 = ODataOpenApiExample.Controllers.V2.PeopleController;
using PeopleControllerV3 = ODataOpenApiExample.Controllers.V3.PeopleController;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // serialize enums as strings in api responses (e.g. Role)
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        // ignore omitted parameters on models to enable optional params (e.g. User update)
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    })
    .AddOData(
        options =>
        {
            options.Count().Select().OrderBy();
            options.RouteOptions.EnableKeyInParenthesis = false;
            options.RouteOptions.EnableNonParenthesisForEmptyParameterFunction = true;
            //options.RouteOptions.EnablePropertyNameCaseInsensitive = true;
            options.RouteOptions.EnableQualifiedOperationCall = false;
            options.RouteOptions.EnableUnqualifiedOperationCall = true;
        });
builder.Services.AddApiVersioning(
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
                    options.AddRouteComponents("api");
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
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(
    options =>
    {
        // add a custom operation filter which sets default values
        options.OperationFilter<SwaggerDefaultValues>();

        string fileName = typeof(Program).Assembly.GetName().Name + ".xml";
        string filePath = Path.Combine(AppContext.BaseDirectory, fileName);

        // integrate xml comments
        options.IncludeXmlComments(filePath);
    });

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    //app.UseMigrationsEndPoint();

    // Initialise and seed database
    using (IServiceScope scope = app.Services.CreateScope())
    {
        ApplicationDbContextInitialiser initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        initialiser.MigrateDatabaseAndSeed();
    }
    // navigate to ~/$odata to determine whether any endpoints did not match an odata route template
    app.UseODataRouteDebug();
}

app.UseSwagger();
app.UseSwaggerUI(
    options =>
    {
        IReadOnlyList<Asp.Versioning.ApiExplorer.ApiVersionDescription> descriptions = app.DescribeApiVersions();

        // build a swagger endpoint for each discovered API version
        foreach (Asp.Versioning.ApiExplorer.ApiVersionDescription description in descriptions)
        {
            string url = $"/swagger/{description.GroupName}/swagger.json";
            string name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();