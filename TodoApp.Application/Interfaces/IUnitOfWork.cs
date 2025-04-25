namespace TodoApp.Application.Interfaces
{
	public interface IUnitOfWork : IDisposable
	{
		ITodoRepository Todo { get; }
		Task Save();
	}
}
