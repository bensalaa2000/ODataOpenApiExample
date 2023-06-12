using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.OData.Batch;
using ODataMappingApi;
using ODataMappingApi.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);
// Add services to the container.

builder.Services.AddControllers()/*.AddNewtonsoftJson()*/.AddOData(options =>
{
    DefaultODataBatchHandler defaultBatchHandler = new DefaultODataBatchHandler();
    defaultBatchHandler.MessageQuotas.MaxNestingDepth = 2;
    defaultBatchHandler.MessageQuotas.MaxOperationsPerChangeset = 10;
    defaultBatchHandler.MessageQuotas.MaxReceivedMessageSize = 100;

    options.EnableQueryFeatures(50000);//.Select().Filter().OrderBy().SetMaxTop(5000).Count().Expand()
    options.AddRouteComponents("odata", ODataExtensions.GetEdmModel(), defaultBatchHandler);
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
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddSwaggerGen(c =>
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

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        ApplicationDbContextInitialiser initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        initialiser.MigrateDatabaseAndSeed();
    }
    // navigate to ~/$odata to determine whether any endpoints did not match an odata route template
    app.UseODataRouteDebug();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseODataQueryRequest();/*TODO : A explorer*/
app.UseODataBatching();
app.UseHttpsRedirection();

app.UseAuthorization();
/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapODataRoute(50000);
});*/
app.MapControllers();

app.Run();
