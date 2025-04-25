using MediatR;
using TodoApp.Application.Dtos;

namespace TodoApp.Application.Features.TodoManager.Request.Queries
{
	public class GetTodoDetailsRequest : IRequest<TodoDetailDto>
	{
		public Guid Id { get; set; }
	}
}
