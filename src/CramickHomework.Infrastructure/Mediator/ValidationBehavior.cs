﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CramickHomework.Infrastructure.Mediator
{
	public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
		where TRequest : IRequest<TResponse>
	{
		private readonly IEnumerable<IValidator<TRequest>> _validators;

		public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		{
			_validators = validators;
		}

		public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			List<ValidationFailure> failures = _validators
				.Select(v => v.Validate(request))
				.SelectMany(result => result.Errors)
				.Where(f => f != null)
				.ToList();

			if (failures.Any())
				throw new ValidationException(failures);

			return next();
		}
	}
}
