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
					Id = Guid.Parse("f547e6ec-e9f1-4464-9d65-8bea192a8c3c"),
					Description = "Todo 1",
					IsComplete = true
				},
				new Todo
				{
					Id = Guid.Parse("c591ef19-c5f0-40a9-b3b5-a424a21f3a24"),
					Description = "Todo 2",
					IsComplete = false
				}
			);

		}
	}
}
