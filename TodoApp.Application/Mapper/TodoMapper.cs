using TodoApp.Application.Dtos;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Mapper
{
	public static class TodoMapper
	{
		public static TodoDetailDto ToDto(this Todo todo)
		{
			return new TodoDetailDto(
				todo.Id,
				todo.Description,
				todo.IsComplete
			);
		}

		public static List<TodoDetailDto> ToDto(this List<Todo> todos)
		{
			return todos.ConvertAll(t => new TodoDetailDto(
				t.Id,
				t.Description,
				t.IsComplete
			));
		}

		//public static IEnumerable<TodoDetailDto> ToDto(this IEnumerable<Todo> todos)
		//{
		//	return todos.Select(todo => new TodoDetailDto(
		//		todo.Id,
		//		todo.Description,
		//		todo.IsComplete
		//	));
		//}

		public static Todo ToEntity(this TodoRequestDto todoDto)
		{
			return new Todo
			{
				Description = todoDto.Description.Trim()
			};
		}
	}
}
