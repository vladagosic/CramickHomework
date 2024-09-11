using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Features.Contacts.Queries;
using FluentValidation;
using MediatR;

namespace CramickHomework.Application.Features.Contacts.Commands
{
	public static partial class CreateOrUpdateContact
	{
		public record Request : IRequest<CreateUpdateResult<GetContacts.ContactModel>>
		{
			public Guid? Id { get; set; }
			public string Name { get; set; } = default!;
			public string? Phone { get; set; }
		}

		public class Validator : AbstractValidator<Request>
		{
			public Validator()
			{
				RuleFor(x => x.Name)
					.NotEmpty()
					.MaximumLength(Constants.User.FullNameLength);

				RuleFor(x => x.Phone)
					.MaximumLength(Constants.User.PhoneLength);
			}
		}
	}
}
