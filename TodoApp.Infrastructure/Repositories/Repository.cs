using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Context;
using TodoApp.Infrastructure.Logs;
using TodoApp.Shared.Responses;

namespace TodoApp.Infrastructure.Repositories;

public class Repository(TodoDbContext todoDbContext) : ITodo
{
	private readonly TodoDbContext _todoDbContext = todoDbContext;

	public Task<Response> CreateAsync(Todo entity)
	{
		try
		{
			if (entity == null)
				return Task.FromResult(new Response(false, "Todo cannot be null.", 204));
			_todoDbContext.Add(entity);
			_todoDbContext.SaveChanges();
			return Task.FromResult(new Response(true, "Todo added successfully.", 200));
		}
		catch (Exception ex)
		{
			LogException.LogExceptions(ex);

			return Task.FromResult(new Response(false, "Error occured while adding the product.", 204));
		}
	}

	public Task<Response> DeleteAsync(Guid id)
	{
		try
		{
			if (id == Guid.Empty)
				return Task.FromResult(new Response(false, "Todo id cannot be empty.", 204));
			var todo = _todoDbContext.Find<Todo>(id);
			if (todo == null)
				return Task.FromResult(new Response(false, "Todo not found.", 204));
			_todoDbContext.Remove(todo);
			_todoDbContext.SaveChanges();
			return Task.FromResult(new Response(true, "Todo deleted successfully.", 200));
		}
		catch (Exception ex)
		{
			LogException.LogExceptions(ex);
			return Task.FromResult(new Response(false, "Error occurred while deleting the product.", 204));
		}
	}

	public async Task<Todo> FindByIdAsync(Guid id)
	{
		try
		{

			var todo = await _todoDbContext.FindAsync<Todo>(id);
			return todo!;
		}
		catch (Exception ex)
		{
			LogException.LogExceptions(ex);
			throw new Exception("An error occurred while finding the Todo.", ex);
		}
	}

	public async Task<IEnumerable<Todo>> GetAllAsync()
	{
		try
		{
			var todos = await _todoDbContext.Set<Todo>().AsNoTracking().ToListAsync();
			return todos;
		}
		catch (Exception ex)
		{
			LogException.LogExceptions(ex);
			throw new Exception("An error occurred while getting all Todos.", ex);
		}
	}

	public Task<Todo> GetByAsync(Expression<Func<Todo, bool>> predicate)
	{
		try
		{
			var todo = _todoDbContext.Set<Todo>().AsNoTracking().FirstOrDefaultAsync(predicate);
			return todo!;
		}
		catch (Exception ex)
		{
			LogException.LogExceptions(ex);
			throw new Exception("An error occurred while getting the Todo.", ex);
		}
	}
	public Task<Response> UpdateAsync(Todo entity)
	{
		try
		{
			if (entity == null)
				return Task.FromResult(new Response(false, "Todo cannot be null.", 204));
			_todoDbContext.Update(entity);
			_todoDbContext.SaveChanges();
			return Task.FromResult(new Response(true, "Todo updated successfully.", 200));
		}
		catch (Exception ex)
		{
			LogException.LogExceptions(ex);
			return Task.FromResult(new Response(false, "Error occurred while updating the product.", 204));
		}
	}
}
