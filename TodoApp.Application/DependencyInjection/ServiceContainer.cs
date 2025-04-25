using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TodoApp.Application.DependencyInjection;

public static class ServiceContainer
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddMediatR(Assembly.GetExecutingAssembly());
		return services;
	}

}