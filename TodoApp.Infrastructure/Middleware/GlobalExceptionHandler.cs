using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TodoApp.Infrastructure.Logs;

namespace TodoApp.Infrastructure.Middleware;

public class GlobalExceptionHandler(RequestDelegate next)
{
	private readonly RequestDelegate _next = next;

	public async Task InvokeAsync(HttpContext httpContext)
	{
		string title = "Error";
		string message = "An error occurred while processing your request.";
		int statusCode = (int)HttpStatusCode.InternalServerError;

		try
		{
			await _next(httpContext);

		}
		catch (Exception e)
		{
			LogException.LogExceptions(e);

			if (e is TaskCanceledException || e is TimeoutException)
			{
				title = "Out of time";
				message = "The request took too long to complete.";
				statusCode = (int)HttpStatusCode.RequestTimeout;
			}



			await ModifyHeader(httpContext, title, message, statusCode);
		}
	}

	private static async Task ModifyHeader(HttpContext httpContext, string title, string message, int statusCode)
	{
		httpContext.Response.ContentType = "application/json";
		await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
		{
			Detail = message,
			Status = statusCode,
			Title = title
		}, CancellationToken.None);

		return;
	}
}

