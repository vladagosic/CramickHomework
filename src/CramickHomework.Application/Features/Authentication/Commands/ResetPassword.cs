using FluentValidation;
using MediatR;

namespace CramickHomework.Application.Features.Authentication.Commands
{
	public static partial class ResetPassword
	{
		public class Request : IRequest<bool>
		{
			public string? Email { get; set; }
			public string? Password { get; set; }
			public string? Token { get; set; }
		}

		public class Validator : AbstractValidator<Request>
		{
			public Validator()
			{
				RuleFor(x => x.Email)
					.EmailAddress()
					.NotEmpty()
					.MaximumLength(256);

				RuleFor(x => x.Password)
					.NotEmpty();

				RuleFor(x => x.Token)
					.NotEmpty();
			}
		}
	}
}
