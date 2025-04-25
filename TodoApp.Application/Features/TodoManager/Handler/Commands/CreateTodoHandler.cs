using FluentValidation.Results;
using MediatR;
using TodoApp.Application.Features.TodoManager.Request.Commands;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mapper;
using TodoApp.Application.Responses;
using TodoApp.Application.Validators;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoManager.Handler.Commands;
public class CreateTodoHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateTodoRequest, BaseCommandResponse>
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;

	public async Task<BaseCommandResponse> Handle(CreateTodoRequest request, CancellationToken cancellationToken)
	{
		BaseCommandResponse response = new();
		TodoValidator validator = new();
		ValidationResult validationResult = await validator.ValidateAsync(request.TodoRequestDto);
		if (!validationResult.IsValid)
		{
			response.Success = false;
			response.Message = "Todo not created";
			response.Errors = validationResult.Errors.ConvertAll(e => e.ErrorMessage);
		}

		Todo todo = request.TodoRequestDto.ToEntity();
		todo = await _unitOfWork.Todo.Add(todo);
		await _unitOfWork.Save();

		response.Id = todo.Id;
		response.Success = true;
		response.Message = "Todo created successfully";

		return response;
	}
}
