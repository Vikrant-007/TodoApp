using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Context.EntityConfigurations;

namespace TodoApp.Infrastructure.Context
{
	public class TodoDbContext(DbContextOptions<TodoDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
		public DbSet<Todo> Todos { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfiguration(new RoleConfiguration());
			modelBuilder.ApplyConfiguration(new UserConfiguration());
			modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
			modelBuilder.ApplyConfiguration(new TodoConfiguration());
		}
	}
}
