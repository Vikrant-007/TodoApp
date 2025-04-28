using TodoApp.Application.Models.Identity;

namespace TodoApp.Application.Interfaces
{
	public interface IAuthService
	{
		Task<AuthResponse> Login(AuthRequest request);
		Task<RegistrationResponse> Register(RegistrationRequest request);

	}
}
