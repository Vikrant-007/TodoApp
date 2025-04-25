using FluentValidation;
using TodoApp.Application.Dtos;

namespace TodoApp.Application.Validators
{
	public class TodoValidator : AbstractValidator<TodoRequestDto>
	{
		public TodoValidator()
		{
			RuleFor(x => x.Description)
				.NotEmpty()
				.WithMessage("Description is required.")
				.MaximumLength(500)
				.WithMessage("Description must not exceed 500 characters.");
		}
	}
}
