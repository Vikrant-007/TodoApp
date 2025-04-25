using Microsoft.EntityFrameworkCore;
using TodoApp.Application.Interfaces;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Infrastructure.Repositories
{
	public class GenericRepository<T>(TodoDbContext dbContext) : IGenericRepository<T> where T : class
	{
		private readonly DbSet<T> _table = dbContext.Set<T>();

		public async Task<T> Add(T entity)
		{
			await _table.AddAsync(entity);
			return entity;
		}

		public async Task Delete(T entity)
		{
			_table.Remove(entity);
		}

		public async Task<bool> Exists(Guid id)
		{
			var entity = await Get(id);
			return entity != null;
		}

		public async Task<T> Get(Guid id)
		{
			return await _table.FindAsync(id);
		}

		public async Task<List<T>> GetAll()
		{
			return await _table.ToListAsync();
		}

		public async Task Update(T entity)
		{
			_table.Update(entity);
		}
	}
}
