using MediatR;
using TodoApp.Application.Dtos;
using TodoApp.Application.Features.TodoManager.Request.Queries;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mapper;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoManager.Handler.Queries
{
	public class GetTodoDetailsHandler(ITodoRepository todo) : IRequestHandler<GetTodoDetailsRequest, TodoDetailDto>
	{
		private readonly ITodoRepository _todo = todo;

		public async Task<TodoDetailDto> Handle(GetTodoDetailsRequest request, CancellationToken cancellationToken)
		{
			Todo todos = await _todo.Get(request.Id);

			return todos.ToDto();
		}
	}
}
