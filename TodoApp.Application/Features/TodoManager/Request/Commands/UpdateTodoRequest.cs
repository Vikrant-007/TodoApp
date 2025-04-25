using MediatR;
using TodoApp.Application.Dtos;

namespace TodoApp.Application.Features.TodoManager.Request.Commands;

public class UpdateTodoRequest : IRequest<Unit>
{
	public Guid Id { get; set; }
	public TodoRequestDto TodoRequestDto { get; set; } = default!;
}
