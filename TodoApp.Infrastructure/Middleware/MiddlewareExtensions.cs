﻿using Microsoft.AspNetCore.Builder;

namespace TodoApp.Infrastructure.Middleware;
public static class MiddlewareExtensions
{
	public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<GlobalExceptionHandler>();
	}
}

