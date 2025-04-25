using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Models;
using TodoApp.Infrastructure.Context;
using TodoApp.Infrastructure.Mail;
using TodoApp.Infrastructure.Middleware;
using TodoApp.Infrastructure.Repositories;

namespace TodoApp.Infrastructure.DependencyInjection;

public static class ServiceContainer
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
	{
		services.AddDbContext<TodoDbContext>(options => options.UseSqlServer(
			config.GetConnectionString("TodoDb"))
		);
		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<ITodoRepository, TodoRepository>();

		Log.Logger = new LoggerConfiguration()
			.MinimumLevel.Information()
			.WriteTo.Debug()
			.WriteTo.Console()
			.WriteTo.File(
				path: config.GetSection("Log:Path").Value!,
				rollingInterval: RollingInterval.Day,
				retainedFileCountLimit: 7,
				fileSizeLimitBytes: 10 * 1024 * 1024
			)
			.CreateLogger();

		services.AddCors(options =>
		{
			options.AddPolicy("AllowAllOrigins",
				builder =>
				{
					builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				});
		});
		services.AddJWTAuthenticationScheme(config);

		services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
		services.AddTransient<IEmailSender, EmailSender>();

		services.AddSwaggerScheme();

		return services;
	}

	public static IApplicationBuilder UseInfrastructurePolicies(this IApplicationBuilder app)
	{
		app.UseGlobalExceptionHandler();
		app.UseCors("AllowAllOrigins");
		app.UseSwaggerPolicies();
		return app;
	}

}