using FluentValidation;
using MediatR;
using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Features.Users.Queries;

namespace CramickHomework.Application.Features.Users.Commands
{
	public static partial class CreateUser
	{
		public class Request : IRequest<CreateUpdateResult<GetUsers.UserModel>>
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
