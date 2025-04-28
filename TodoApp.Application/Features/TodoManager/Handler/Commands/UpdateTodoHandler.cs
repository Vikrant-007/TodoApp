using FluentValidation.Results;
using MediatR;
using TodoApp.Application.Exceptions;
using TodoApp.Application.Features.TodoManager.Request.Commands;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mapper;
using TodoApp.Application.Validators;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoManager.Handler.Commands;

public class UpdateTodoHandler(ITodoRepository todo) : IRequestHandler<UpdateTodoRequest, Unit>
{
	private readonly ITodoRepository _todo = todo;

	public async Task<Unit> Handle(UpdateTodoRequest request, CancellationToken cancellationToken)
	{

		Todo? todo = await _todo.Get(request.Id) ?? throw new NotFoundException(nameof(Todo), request.Id);
		todo = request.TodoRequestDto.ToEntity();
		await _todo.Update(todo);
		return Unit.Value;
	}
}
