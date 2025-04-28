using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Context.EntityConfigurations
{
	public class TodoConfiguration : IEntityTypeConfiguration<Todo>
	{
		public void Configure(EntityTypeBuilder<Todo> builder)
		{
			builder.HasData(
				new Todo
				{
					Id = Guid.NewGuid(),
					Description = "Todo 1",
					IsComplete = true,
					UserId = "9e224968-33e4-4652-b7b7-8574d048cdb9"
				},
				new Todo
				{
					Id = Guid.NewGuid(),
					Description = "Todo 2",
					IsComplete = false,
					UserId = "9e224968-33e4-4652-b7b7-8574d048cdb9"
				}
			);
		}
	}
}
