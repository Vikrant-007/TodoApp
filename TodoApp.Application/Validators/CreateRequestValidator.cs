using FluentValidation;
using System.Reflection;

namespace TodoApp.Application.Validators;

public class CreateRequestValidator<T> : AbstractValidator<CreateEditRequest<T>>
{
	private readonly IRequiredFieldService _requiredFieldsService;

	public CreateRequestValidator(IRequiredFieldService requiredFieldsService)
	{
		_requiredFieldsService = requiredFieldsService;

		var genericType = typeof(T).Name;

		List<string> requiredFields = _requiredFieldsService.GetFieldsByEntityType(genericType);

		foreach (var field in requiredFields)
		{
			RuleFor(x => GetPropertyValue(x, field)).NotEmpty().WithMessage($"The {field} field is required.").OverridePropertyName(field);
		}
	}

	public CreateRequestValidator(IRequiredFieldService requiredFieldsService, List<string> propertiesToValidate)
	{
		_requiredFieldsService = requiredFieldsService;

		var genericType = typeof(T).Name;

		List<string> requiredFields = _requiredFieldsService.GetFieldsByEntityType(genericType);

		if (propertiesToValidate.Any())
		{
			requiredFields = [.. requiredFields.Where(field => propertiesToValidate.Contains(field))];
		}

		foreach (var field in requiredFields)
		{
			RuleFor(x => GetPropertyValue(x, field)).NotEmpty().WithMessage($"The {field} field is required.").OverridePropertyName(field);
		}
	}

	private object GetPropertyValue(CreateEditRequest<T> model, string propertyName)
	{
		var propertyInfo = model.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
		return propertyInfo?.GetValue(model, null);
	}
}