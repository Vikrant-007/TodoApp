namespace TodoApp.Application.Models.Identity
{
    public class AuthResponse
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string Token { get; set; } = default!;
	}
}
