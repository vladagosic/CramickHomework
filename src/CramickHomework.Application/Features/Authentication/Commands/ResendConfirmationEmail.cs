using FluentValidation;
using MediatR;

namespace CramickHomework.Application.Features.Authentication.Commands
{
	public static partial class ResendConfirmationEmail
	{
		public class Request : IRequest<bool>
		{
			public string? Email { get; set; }
		}

		public class Validator : AbstractValidator<Request>
		{
			public Validator()
			{
				RuleFor(x => x.Email)
					.EmailAddress()
					.NotEmpty()
					.MaximumLength(256);
			}
		}
	}
}
