using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using TodoApp.Application.Exceptions;
using TodoApp.Infrastructure.Logs;
using TodoApp.Shared.Responses;

namespace TodoApp.Infrastructure.Middleware;

public class GlobalExceptionHandler(RequestDelegate next)
{
	private readonly RequestDelegate _next = next;

	public async Task InvokeAsync(HttpContext httpContext)
	{
		try
		{
			await _next(httpContext);

		}
		catch (Exception ex)
		{
			LogException.LogExceptions(ex);

			await HandleExceptionAsync(httpContext, ex);
		}
	}

	private Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		context.Response.ContentType = "application/json";
		HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
		BaseResponse<string> result = new(false, exception.Message, 204);

		switch (exception)
		{
			case BadRequestException badRequestException:
				statusCode = HttpStatusCode.BadRequest;
				result.Error = exception.Message;
				break;
			case ValidationException validationException:
				statusCode = HttpStatusCode.BadRequest;
				result.Error = string.Join(", ", validationException.Errors);
				break;
			case NotFoundException notFoundException:
				statusCode = HttpStatusCode.NotFound;
				result.Error = exception.Message;
				break;
			default:
				break;
		}

		context.Response.StatusCode = (int)statusCode;
		return context.Response.WriteAsJsonAsync(result);
	}


	public class ErrorDeatils
	{
		public string ErrorType { get; set; } = default!;
		public string ErrorMessage { get; set; } = default!;
	}
}
