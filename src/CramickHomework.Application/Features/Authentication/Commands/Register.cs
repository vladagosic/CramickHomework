using FluentValidation;
using MediatR;

namespace CramickHomework.Application.Features.Authentication.Commands
{
	public static partial class Register
	{
		public class Request : IRequest<string?> 
		{
			public string? FullName { get; set; }
			public string? Email { get; set; }
			public string? Password { get; set; }
		}

		public class Validator : AbstractValidator<Request>
		{
			public Validator()
			{
				RuleFor(x => x.FullName)
					.NotEmpty()
					.MaximumLength(Constants.User.FullNameLength);

				RuleFor(x => x.Email)
					.EmailAddress()
					.NotEmpty()
					.MaximumLength(Constants.User.EmailLength);

				RuleFor(x => x.Password)
					.NotEmpty();
			}
		}
	}
}
