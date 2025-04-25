using System.Linq.Expressions;
using TodoApp.Shared.Responses;

namespace TodoApp.Application.Interfaces
{
	public interface IGenericInterface<T> where T : class
	{
		Task<T> FindByIdAsync(Guid id);
		Task<IEnumerable<T>> GetAllAsync();
		Task<Response> CreateAsync(T entity);
		Task<Response> UpdateAsync(T entity);
		Task<Response> DeleteAsync(Guid id);
		Task<T> GetByAsync(Expression<Func<T, bool>> predicate);
	}
}
