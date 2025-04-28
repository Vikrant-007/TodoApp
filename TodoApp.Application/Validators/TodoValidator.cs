using FluentValidation;
using TodoApp.Application.Dtos;
using TodoApp.Application.Features.TodoManager.Request.Commands;

namespace TodoApp.Application.Validators
{
	public class TodoValidator : AbstractValidator<CreateTodoRequest>
	{
		public TodoValidator()
		{
			RuleFor(x => x.TodoRequestDto.Description)
				.NotEmpty()
				.WithMessage("Description is required.")
				.MinimumLength(5)
				.WithMessage("Description must be more than 5 character.")
				.MaximumLength(500)
				.WithMessage("Description must not exceed 500 characters.");
		}
	}
}
