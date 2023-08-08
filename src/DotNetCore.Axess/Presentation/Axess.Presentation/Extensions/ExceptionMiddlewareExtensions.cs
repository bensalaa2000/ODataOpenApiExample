using Axess.Presentation.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Axess.Presentation.Extensions;

public static class ExceptionMiddlewareExtensions
{
	public static void UseExceptionMiddleware(this IApplicationBuilder app)
	{
		// TODO : En double , faire un choix
		app.UseMiddleware<ExceptionMiddleware>();
		///app.UseMiddleware<ErrorHandlerMiddleware>();
	}
}