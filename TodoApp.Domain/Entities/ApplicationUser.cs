using Microsoft.AspNetCore.Identity;

namespace TodoApp.Domain.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public string FirstName { get; set; } = default!;
		public string LastName { get; set; } = default!;
		public virtual ICollection<Todo> Todos { get; set; } = [];
	}
}
