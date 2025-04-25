using Microsoft.AspNetCore.Identity;

namespace TodoApp.Domain.Entities
{
	public class ApplicationUser : IdentityUser
	{
		public virtual ICollection<Todo> Todos { get; set; } = [];
	}
}
