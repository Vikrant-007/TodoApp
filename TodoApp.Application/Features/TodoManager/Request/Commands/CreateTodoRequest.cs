using MediatR;
using TodoApp.Application.Dtos;
using TodoApp.Application.Responses;

namespace TodoApp.Application.Features.TodoManager.Request.Commands;

public class CreateTodoRequest : IRequest<BaseCommandResponse>
{
	public TodoRequestDto TodoRequestDto { get; set; } = default!;
}
