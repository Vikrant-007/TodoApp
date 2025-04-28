using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TodoApp.Application.Models.Identity;

namespace TodoApp.Infrastructure.DependencyInjection;

public static class JWTAuthenticationScheme
{
	public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
	{

		services.Configure<JwtSettings>(config.GetSection("JwtSettings"));

		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer("Bearer", options =>
			{
				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = config.GetSection("JwtSettings:Issuer").Value!,
					ValidAudience = config.GetSection("JwtSettings:Audience").Value!,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JwtSettings:Key").Value!)),
					ClockSkew = TimeSpan.Zero,
				};
			});

		return services;
	}
}
