using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Models.Identity;

namespace TodoApp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController(IAuthService authenticationService) : ControllerBase
	{
		private readonly IAuthService _authenticationService = authenticationService;

		[HttpPost("login")]
		public async Task<ActionResult<AuthResponse>> Login([FromForm] AuthRequest request)
		{
			return Ok(await _authenticationService.Login(request));
		}

		[HttpPost("register")]
		public async Task<ActionResult<RegistrationResponse>> Register([FromForm] RegistrationRequest request)
		{
			return Ok(await _authenticationService.Register(request));
		}
	}
}
