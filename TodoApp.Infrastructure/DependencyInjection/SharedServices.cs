using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Serilog;
using TodoApp.Infrastructure.Middleware;

namespace TodoApp.Infrastructure.DependencyInjection
{
	public static class SharedServices
	{
		public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services, IConfiguration config, string filename) where TContext : DbContext
		{

			services.AddDbContext<TContext>(options => options.UseSqlServer(
				config.GetConnectionString("TodoDb"))
			);

			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Information()
				.WriteTo.Debug()
				.WriteTo.Console()
				.WriteTo.File(
					path: filename,
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

			services.AddSwaggerScheme();
			services.AddJWTAuthenticationScheme(config);
		
			return services;
		}

		public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder app)
		{
			app.UseGlobalExceptionHandler();
			app.UseCors("AllowAllOrigins");
			app.UseSwaggerPolicies();
			return app;
		}

	}
}