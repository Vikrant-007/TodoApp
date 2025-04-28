using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
	public interface ITodoRepository : IGenericRepository<Todo>
	{
		Task<List<Todo>> FilterSearch(string? search, bool? isCompleted, int pageNumber, int pageSize);
	}
}
