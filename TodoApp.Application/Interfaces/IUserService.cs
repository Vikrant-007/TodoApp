using TodoApp.Application.Models.Identity;

namespace TodoApp.Application.Interfaces
{
	public interface IUserService
	{
		Task<User> GetUser(string userId);
		Task<List<User>> GetUsers();
	}
}
