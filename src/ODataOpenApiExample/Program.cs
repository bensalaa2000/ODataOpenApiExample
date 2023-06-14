using Axess;
using Axess.Application;
using Axess.Infrastructure;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.UriParser;
using System.Reflection;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

(typeof(ODataUriResolver)
              .GetField("Default", BindingFlags.Static | BindingFlags.NonPublic)!
              .GetValue(null) as ODataUriResolver)!
              .EnableCaseInsensitive = true;

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

app.UseODataQueryRequest();
app.UseODataBatching();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();