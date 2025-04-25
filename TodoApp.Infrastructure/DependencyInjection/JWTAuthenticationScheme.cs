using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TodoApp.Infrastructure.DependencyInjection;

public static class JWTAuthenticationScheme
{
	public static IServiceCollection AddJWTAuthenticationScheme(this IServiceCollection services, IConfiguration config)
	{
		services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer("Bearer", options =>
			{
				byte[] key = Encoding.UTF8.GetBytes(config.GetSection("Jwt:Key").Value!);
				string issuer = config.GetSection("Jwt:Issuer").Value!;
				string audience = config.GetSection("Jwt:Audience").Value!;

				options.RequireHttpsMetadata = false;
				options.SaveToken = true;
				options.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = issuer,
					ValidAudience = audience,
					IssuerSigningKey = new SymmetricSecurityKey(key),
				};
			});

		return services;
	}
}
