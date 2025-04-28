using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Context.EntityConfigurations
{
	public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			var hasher = new PasswordHasher<ApplicationUser>();
			builder.HasData(
				 new ApplicationUser
				 {
					 Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
					 Email = "admin@admin.com",
					 NormalizedEmail = "ADMIN@LOCALHOST.COM",
					 FirstName = "System",
					 LastName = "Admin",
					 UserName = "admin@localhost.com",
					 NormalizedUserName = "ADMIN@LOCALHOST.COM",
					 PasswordHash = hasher.HashPassword(null, "P@ssword1"),
					 EmailConfirmed = true
				 },
				 new ApplicationUser
				 {
					 Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
					 Email = "vicky@user.com",
					 NormalizedEmail = "VICKY@USER.COM",
					 FirstName = "System",
					 LastName = "User",
					 UserName = "vicky@user.com",
					 NormalizedUserName = "VICKY@USER.COM",
					 PasswordHash = hasher.HashPassword(null, "P@ssword1"),
					 EmailConfirmed = true
				 }
			);
		}
	}
}
