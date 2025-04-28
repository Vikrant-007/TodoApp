using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Dtos;
using TodoApp.Application.Features.TodoManager.Request.Commands;
using TodoApp.Application.Features.TodoManager.Request.Queries;
using TodoApp.Shared.Responses;

namespace TodoApp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[Authorize]
	public class TodoController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpGet]
		public async Task<BaseResponse<List<TodoDetailDto>>> GetAll()
		{
			List<TodoDetailDto> todos = await _mediator.Send(new GetTodosListRequest());

			if(todos == null || todos.Count == 0)
				return BaseResponse<List<TodoDetailDto>>.Failure("Data Not Found");

			return BaseResponse<List<TodoDetailDto>>.Success(todos);
		}

		[HttpPost]
		public async Task<TodoDetailDto> Post([FromForm] TodoRequestDto todoRequestDto)
		{
			var response = await _mediator.Send(new CreateTodoRequest() { TodoRequestDto = todoRequestDto });
			return response;
		}
	}



}
