using MediatR;

namespace TodoApp.Application.Features.TodoManager.Request.Commands;

public class DeleteTodoRequest : IRequest<Unit>
{
	public Guid Id { get; set; }
}