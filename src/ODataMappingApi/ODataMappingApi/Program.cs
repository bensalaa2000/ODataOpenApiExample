using Axess.Application;
using Axess.Infrastructure;
using Microsoft.AspNetCore.OData;
using ODataMappingApi;
using Axess.Infrastructure.Contexts;

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
