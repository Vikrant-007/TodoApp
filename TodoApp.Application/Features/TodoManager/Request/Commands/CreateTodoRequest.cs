using MediatR;
using TodoApp.Application.Dtos;

namespace TodoApp.Application.Features.TodoManager.Request.Commands;

public class CreateTodoRequest : IRequest<TodoDetailDto>
{
	public TodoRequestDto TodoRequestDto { get; set; } = default!;
}
