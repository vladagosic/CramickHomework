using FluentValidation;
using MediatR;

namespace CramickHomework.Application.Features.Authentication.Commands
{
	public static partial class Login
	{
		public class Request : IRequest<string>
		{
			public string? Email { get; set; }
			public string? Password { get; set; }
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
			}
		}
	}
}
