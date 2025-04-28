using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TodoApp.Application.Behaviors;
using TodoApp.Application.Validators;

namespace TodoApp.Application.DependencyInjection;

public static class ServiceContainer
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		services.AddMediatR(Assembly.GetExecutingAssembly());

		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		return services;

		//services.AddFluentValidationAutoValidation();
		//services.AddFluentValidationClientsideAdapters();
		//services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		//services.AddControllers(option => option.Filters.Add<ValidationMiddleware>());
		//services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

		return services;
	}

}