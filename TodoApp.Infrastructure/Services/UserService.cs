using Microsoft.AspNetCore.Identity;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Models.Identity;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Services
{
	public class UserService(UserManager<ApplicationUser> userManager) : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager = userManager;

		public async Task<User> GetUser(string userId)
		{
			ApplicationUser user = await _userManager.FindByIdAsync(userId);
			return new User
			{
				Email = user.Email!,
				Id = user.Id,
				Firstname = user.FirstName,
				Lastname = user.LastName
			};
		}

		public async Task<List<User>> GetUsers()
		{
			IList<ApplicationUser> users = await _userManager.GetUsersInRoleAsync("User");
			return [.. users.Select(q => new User
			{
				Id = q.Id,
				Email = q.Email!,
				Firstname = q.FirstName,
				Lastname = q.LastName
			})];
		}
	}
}
