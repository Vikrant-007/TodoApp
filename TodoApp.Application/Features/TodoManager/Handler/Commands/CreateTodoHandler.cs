using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TodoApp.Application.Dtos;
using TodoApp.Application.Features.TodoManager.Request.Commands;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Mapper;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Features.TodoManager.Handler.Commands;
public class CreateTodoHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor) : IRequestHandler<CreateTodoRequest, TodoDetailDto>
{
	private readonly IUnitOfWork _unitOfWork = unitOfWork;
	private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

	public async Task<TodoDetailDto> Handle(CreateTodoRequest request, CancellationToken cancellationToken)
	{

		Todo todo = request.TodoRequestDto.ToEntity();

		string userId = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(
					q => q.Type == ClaimTypes.NameIdentifier)!.Value;

		todo.UserId = userId;
		todo = await _unitOfWork.Todo.Add(todo);
		//await _unitOfWork.Save();

		return todo.ToDto();
	}
}
