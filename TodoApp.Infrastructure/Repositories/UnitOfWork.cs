using TodoApp.Application.Interfaces;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Infrastructure.Repositories
{
	public class UnitOfWork(TodoDbContext context) : IUnitOfWork
	{
		private readonly TodoDbContext _context = context;
		private ITodoRepository? _todo;

		public ITodoRepository Todo
		{
			get
			{
				_todo ??= new TodoRepository(_context);
				return _todo;
			}
		}

		public void Dispose()
		{
			_context.Dispose();
			GC.SuppressFinalize(this);
		}

		public async Task Save()
		{
			//var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

			await _context.SaveChangesAsync();
		}
	}
}
