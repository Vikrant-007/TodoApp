using MediatR;
using TodoApp.Application.Dtos;

namespace TodoApp.Application.Features.TodoManager.Request.Queries
{
	public class GetTodosListRequest : IRequest<List<TodoDetailDto>>
	{
		public string? Search { get; set; } = string.Empty;
		public bool? IsCompleted { get; set; } = null;
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = 1000;
	}
}
