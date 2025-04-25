using MediatR;
using TodoApp.Application.Dtos;

namespace TodoApp.Application.Features.TodoManager.Request.Queries
{
	public class GetTodosListRequest : IRequest<List<TodoDetailDto>>
	{
		
	}
}
