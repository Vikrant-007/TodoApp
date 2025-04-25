using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Application.Interfaces;
using TodoApp.Infrastructure.Context;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Infrastructure.DependencyInjection;

public static class ServiceContainer
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
	{
		services.AddSharedServices<TodoDbContext>(config, "Logs/TodoApiLogger.txt");

		services.AddScoped<ITodo, Repository>();

		return services;
	}

	public static IApplicationBuilder UseInfrastructurePolicies(this IApplicationBuilder app)
	{
		app.UseSharedPolicies();
		return app;
	}

}