using MediatR;
using TodoApp.Application.Dtos;
using TodoApp.Application.Features.TodoManager.Request.Queries;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mapper;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoManager.Handler.Queries
{
	public class GetTodosListHandler(ITodoRepository todo) : IRequestHandler<GetTodosListRequest, List<TodoDetailDto>>
	{
		private readonly ITodoRepository _todo = todo;

		public async Task<List<TodoDetailDto>> Handle(GetTodosListRequest request, CancellationToken cancellationToken)
		{
			List<Todo> todos = await _todo.GetAll();

			return todos.ToDto();
		}
	}
}
