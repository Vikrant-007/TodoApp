using FluentValidation;
using FluentValidation.Results;
using MediatR;
using TodoApp.Application.Validators;

namespace TodoApp.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators, IRequiredFieldService requiredFieldsService) : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> _validators = validators;
	private readonly IRequiredFieldService _requiredFieldsService = requiredFieldsService;

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		try
		{
			var genericType = GetCreateEditRequestSubclassGenericType(request);
			var validationContext = new ValidationContext<TRequest>(request);

			if (genericType != null)
			{
				List<string> validateFields = request.GetType().GetProperty("ValidateFields")?.GetValue(request) as List<string>;
				Type validatorType = typeof(CreateRequestValidator<>).MakeGenericType(genericType);
				var validator = validateFields != null && validateFields.Count > 0 ?
					Activator.CreateInstance(validatorType, _requiredFieldsService, validateFields) : Activator.CreateInstance(validatorType, _requiredFieldsService);

				var validationResult = ((IValidator<TRequest>)validator).Validate(validationContext);

				if (!validationResult.IsValid)
				{
					throw new ValidationException(validationResult.Errors);
				}
			}

			var failures = _validators
				.Select(v => v.Validate(validationContext))
				.SelectMany(result => result.Errors)
				.Where(f => f != null)
				.GroupBy(
				x => x.PropertyName,
				x => x.ErrorMessage,
				(propertyName, errorMessages) => new
				{
					Key = propertyName,
					Values = errorMessages.Distinct().ToArray()
				}).Select(x => new ValidationFailure(x.Key, string.Join(",", x.Values)))
				.ToList();


			if (failures.Any())
				throw new ValidationException(failures);

			return await next();
		}
		catch (Exception ex)
		{
			throw;
		}
	}

	public Type GetCreateEditRequestSubclassGenericType<T>(T request)
	{
		Type requestType = request.GetType();

		while (requestType != null && (!requestType.IsGenericType || requestType.GetGenericTypeDefinition() != typeof(CreateEditRequest<>)))
		{
			requestType = requestType.BaseType;
		}

		if (requestType == null)
		{
			return null;
		}

		Type genericTypeArgument = requestType.GetGenericArguments()[0];

		return genericTypeArgument;
	}
}