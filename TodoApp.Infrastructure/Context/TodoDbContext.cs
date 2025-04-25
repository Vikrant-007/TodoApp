using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Context
{
	public class TodoDbContext(DbContextOptions<TodoDbContext> options) : IdentityDbContext<ApplicationUser>(options)
	{
		public DbSet<Todo> Todos { get; set; } = null!;

		protected override void OnModelCreating(ModelBuilder builder)
		{

			base.OnModelCreating(builder);
			string readerRoleId = "4405523-93a0-4366-a877-64e55655fde6";
			string writerRoleId = "48cb80d4-bf42-4c60-bd6d-14c44b27d7b0";

			List<IdentityRole> role =
			[
				new IdentityRole
				{
					Id = readerRoleId,
					ConcurrencyStamp = readerRoleId,
					Name = "Reader",
					NormalizedName = "Reader".ToUpper()
				},
				new IdentityRole
				{
					Id = writerRoleId,
					ConcurrencyStamp = writerRoleId,
					Name = "Writer",
					NormalizedName = "Writer".ToUpper()
				},
			];

			builder.Entity<IdentityRole>().HasData(role);
		}
	}
}
