using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TodoApp.Application.Dtos;
using TodoApp.Application.Features.TodoManager.Request.Commands;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mapper;
using TodoApp.Application.Validators;
using TodoApp.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TodoApp.Application.Features.TodoManager.Handler.Commands;
public class CreateTodoHandler(IUnitOfWork unitOfWork, IValidator<TodoRequestDto> validator, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateTodoRequest, TodoDetailDto>
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IValidator<TodoRequestDto> _validator = validator;
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	public async Task<TodoDetailDto> Handle(CreateTodoRequest request, CancellationToken cancellationToken)
	{
		_validator.ValidateAndThrow(request.TodoRequestDto);
		//TodoValidator validator = new();
		//ValidationResult validationResult = validator.Validate(request.TodoRequestDto);

		//if (!validationResult.IsValid)
		//	throw new ValidationException(validationResult);

		Todo todo = request.TodoRequestDto.ToEntity();

		string userId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(
					q => q.Type == ClaimTypes.NameIdentifier)!.Value;

		todo.UserId = userId;
		todo = await _unitOfWork.Todo.Add(todo);
		//await _unitOfWork.Save();

		return todo.ToDto();
	}
}
