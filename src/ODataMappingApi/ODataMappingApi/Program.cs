using Axess.Api.OpenApi;
using Axess.Application;
using Axess.Infrastructure;
using Axess.Infrastructure.Contexts;
using Microsoft.AspNetCore.OData;
using ODataMappingApi;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (IServiceScope scope = app.Services.CreateScope())
    {
        ApplicationDbContextInitialiser initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.MigrateDatabaseAndSeedAsync();
    }
    // navigate to ~/$odata to determine whether any endpoints did not match an odata route template
    app.UseODataRouteDebug();

    // If you want to use /$openapi, enable the middleware.
    app.UseODataOpenApi();

    // Add OData /$query middleware
    app.UseODataQueryRequest();

    // Add the OData Batch middleware to support OData $Batch
    app.UseODataBatching();

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OData 8.x OpenAPI");
        ////c.SwaggerEndpoint("/$openapi", "OData raw OpenAPI");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapODataRoute(50000);
});*/
app.MapControllers();

app.Run();
