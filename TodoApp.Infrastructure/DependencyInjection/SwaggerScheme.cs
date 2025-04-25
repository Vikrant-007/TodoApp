using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace TodoApp.Infrastructure.DependencyInjection;

public static class SwaggerScheme
{
	public static IServiceCollection AddSwaggerScheme(this IServiceCollection services)
	{
		services.AddSwaggerGen(option =>
		{
			option.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
			option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
			{
				In = ParameterLocation.Header,
				Description = "Please enter token",
				Name = "Authorization",
				Type = SecuritySchemeType.Http,
				BearerFormat = "JWT",
				Scheme = "bearer"
			});
			option.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type=ReferenceType.SecurityScheme,
								Id="Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
		});

		return services;
	}


	public static IApplicationBuilder UseSwaggerPolicies(this IApplicationBuilder app)
	{

		app.UseSwagger();
		app.UseSwaggerUI(option =>
		{
			option.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
		});

		return app;
	}
}
