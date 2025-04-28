using Microsoft.AspNetCore.Http;
using System.Net;
using TodoApp.Application.Exceptions;
using TodoApp.Infrastructure.Logger;
using TodoApp.Shared.Responses;

namespace TodoApp.Infrastructure.Middleware;

public class ExceptionHandlerMiddleware : IMiddleware
{
	
	public async Task InvokeAsync(HttpContext context, RequestDelegate next)
	{
		try
		{
			await next(context);

		}
		catch (Exception ex)
		{
			LogException.LogExceptions(ex);

			await HandleExceptionAsync(context, ex);
		}
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception exception)
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

}
