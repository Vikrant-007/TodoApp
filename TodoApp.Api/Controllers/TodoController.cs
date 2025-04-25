using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Dtos;
using TodoApp.Application.Mapper;
using TodoApp.Application.Interfaces;

namespace TodoApp.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TodoController(ITodo todo) : ControllerBase
	{
		private readonly ITodo _todo = todo;

		[HttpGet]
		public async Task<IEnumerable<TodoDetailDto>> GetAll()
		{
			var todos = await _todo.GetAllAsync();
			if (!todos.Any())
				return [];

			return todos.ToDto();
		}
	}
}
