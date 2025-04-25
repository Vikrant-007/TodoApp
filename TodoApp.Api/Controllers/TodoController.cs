using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Dtos;
using TodoApp.Application.Mapper;
using TodoApp.Application.Interfaces;
using MediatR;
using TodoApp.Application.Features.TodoManager.Request.Queries;

namespace TodoApp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoController(IMediator mediator) : ControllerBase
	{
		private readonly IMediator _mediator = mediator;

		[HttpGet]
		public async Task<List<TodoDetailDto>> GetAll()
		{
			return await _mediator.Send(new GetTodosListRequest());
		}
	}
}
