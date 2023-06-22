using Axess.Api;
using Axess.Application;
using Axess.Infrastructure;
using Axess.Presentation.Middlewares;
using DotNetCore.Axess.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.UriParser;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebUIServices();

var app = builder.Build();
/// app.Configure();
// Configure the HTTP request pipeline.

(typeof(ODataUriResolver)
			  .GetField("Default", BindingFlags.Static | BindingFlags.NonPublic)!
			  .GetValue(null) as ODataUriResolver)!
			  .EnableCaseInsensitive = true;

if (app.Environment.IsDevelopment())
{
	////app.UseMigrationsEndPoint();

	// Initialise and seed database
	using (var scope = app.Services.CreateScope())
	{
		var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
		initialiser.MigrateDatabaseAndSeed();
	}
	// navigate to ~/$odata to determine whether any endpoints did not match an odata route template
	app.UseODataRouteDebug();
}

app.UseSwagger();
app.UseSwaggerUI(
	options =>
	{
		var descriptions = app.DescribeApiVersions();

		// build a swagger endpoint for each discovered API version
		foreach (var groupName in descriptions.Select(x => x.GroupName))
		{
			var url = $"/swagger/{groupName}/swagger.json";
			var name = groupName.ToUpperInvariant();
			options.SwaggerEndpoint(url, name);
		}
	});

app.UseODataQueryRequest();
app.UseODataBatching();

app.UseExceptionMiddleware();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();