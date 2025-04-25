using MediatR;
using TodoApp.Application.Exceptions;
using TodoApp.Application.Features.TodoManager.Request.Commands;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoManager.Handler.Commands;

public class UpdateTodoStatusHandler(ITodoRepository todo) : IRequestHandler<UpdateTodoStatusRequest, Unit>
{
	private readonly ITodoRepository _todo = todo;

	public async Task<Unit> Handle(UpdateTodoStatusRequest request, CancellationToken cancellationToken)
	{
		Todo todo = await _todo.Get(request.Id) ?? throw new NotFoundException(nameof(Todo), request.Id);
		todo.IsComplete = !todo.IsComplete;
		await _todo.Update(todo);
		return Unit.Value;
	}
}
