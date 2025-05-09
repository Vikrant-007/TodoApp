﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Models.Identity;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Services
{
	public class AuthService(UserManager<ApplicationUser> userManager,
		IOptions<JwtSettings> jwtSettings,
		SignInManager<ApplicationUser> signInManager) : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager = userManager;
		private readonly SignInManager<ApplicationUser> _signInManager = signInManager;
		private readonly JwtSettings _jwtSettings = jwtSettings.Value;

		public async Task<AuthResponse> Login(AuthRequest request)
		{
			var user = await _userManager.FindByEmailAsync(request.Email) ?? throw new Exception($"User with {request.Email} not found.");
			var result = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password, false, lockoutOnFailure: false);

			if (!result.Succeeded)
			{
				throw new Exception($"Credentials for '{request.Email} aren't valid'.");
			}

			JwtSecurityToken jwtSecurityToken = await GenerateToken(user);

			AuthResponse response = new()
			{
				Id = user.Id,
				Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				Email = user.Email!,
				UserName = user.UserName!
			};

			return response;
		}

		public async Task<RegistrationResponse> Register(RegistrationRequest request)
		{
			var existingUser = await _userManager.FindByNameAsync(request.UserName);

			if (existingUser != null)
			{
				throw new Exception($"Username '{request.UserName}' already exists.");
			}

			ApplicationUser user = new()
			{
				Email = request.Email,
				FirstName = request.FirstName,
				LastName = request.LastName,
				UserName = request.UserName,
				EmailConfirmed = true
			};

			var existingEmail = await _userManager.FindByEmailAsync(request.Email);

			if (existingEmail == null)
			{
				var result = await _userManager.CreateAsync(user, request.Password);

				if (result.Succeeded)
				{
					await _userManager.AddToRoleAsync(user, "User");
					return new RegistrationResponse() { UserId = user.Id };
				}
				else
				{
					throw new Exception($"{result.Errors}");
				}
			}
			else
			{
				throw new Exception($"Email {request.Email} already exists.");
			}
		}

		private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
		{
			IList<Claim> userClaims = await _userManager.GetClaimsAsync(user);
			IList<string> roles = await _userManager.GetRolesAsync(user);

			List<Claim> roleClaims = [];

			for (int i = 0; i < roles.Count; i++)
			{
				roleClaims.Add(new Claim(ClaimTypes.Role, roles[i]));
			}

			var claims = new[]
			{
				new Claim(ClaimTypes.Email, user.Email!),
				new Claim(ClaimTypes.NameIdentifier, user.Id),
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
				signingCredentials: signingCredentials);
			return jwtSecurityToken;
		}
	}
}
