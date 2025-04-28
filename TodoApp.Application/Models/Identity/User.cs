namespace TodoApp.Application.Models.Identity
{
	public class User
	{
		public string Id { get; set; } = default!;
		public string Email { get; set; } = default!;
		public string Firstname { get; set; } = default!;
		public string Lastname { get; set; } = default!;
	}
}
