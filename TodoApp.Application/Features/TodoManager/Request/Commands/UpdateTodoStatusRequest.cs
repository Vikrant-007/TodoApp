using MediatR;
using TodoApp.Application.Dtos;

namespace TodoApp.Application.Features.TodoManager.Request.Commands;

public class UpdateTodoStatusRequest : IRequest<Unit>
{
	public Guid Id { get; set; }
}
