using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Infrastructure.Repositories
{
	public class TodoRepository(TodoDbContext todoDbContext) : GenericRepository<Todo>(todoDbContext), ITodoRepository
	{
		public async Task<List<Todo>> FilterSearch(string? search, bool? isCompleted, int pageNumber, int pageSize)
		{
			IQueryable<Todo> todos = await GetAll();

			if (!string.IsNullOrWhiteSpace(search))
				todos = todos.Where(t => t.Description.Contains(search));

			if (isCompleted.HasValue)
				todos = todos.Where(t => t.IsComplete == isCompleted);

			int skipResult = (pageNumber - 1) * pageSize;

			return await todos.Skip(skipResult).Take(pageSize).ToListAsync();
		}

	}
}
