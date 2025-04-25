using MediatR;
using TodoApp.Application.Exceptions;
using TodoApp.Application.Features.TodoManager.Request.Commands;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoManager.Handler.Commands;
public class DeleteTodoHandler(ITodoRepository todo) : IRequestHandler<DeleteTodoRequest, Unit>
{
	private readonly ITodoRepository _todo = todo;

	public async Task<Unit> Handle(DeleteTodoRequest request, CancellationToken cancellationToken)
	{

		Todo? todo = await _todo.Get(request.Id) ?? throw new NotFoundException(nameof(Todo), request.Id);
		await _todo.Delete(todo);

		return Unit.Value;
	}
}
