using Axess.Common.Application.Exceptions;
using Axess.Shared;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace Axess.Presentation.Middlewares;
public class ExceptionMiddleware
{
	private readonly RequestDelegate _next;

	public ExceptionMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await _next(httpContext);
		}
		catch (Exception e)
		{
			await HandleExceptionAsync(httpContext, e);
		}
	}

	private async Task HandleExceptionAsync(HttpContext httpContext, Exception e)
	{
		httpContext.Response.ContentType = MediaTypeNames.Application.Json;
		httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		_ = e.Message;
		var message = "";

		switch (e)
		{
			case ValidationException validationException:
				var errors = validationException.Errors.Select(x => x.Value.FirstOrDefault()).ToList();
				message = string.Join(Environment.NewLine, errors);
				httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				break;

			case UnauthorizedAccessException:
				message = e.Message;
				httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
				break;

			case NotFoundException:
				message = e.Message;
				httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
				break;

			default:
				message = e.Message;
				httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
				break;
		}

		var result = JsonSerializer.Serialize(new ApiResult<string>(false, message));

		await httpContext.Response.WriteAsync(result);
	}
}
