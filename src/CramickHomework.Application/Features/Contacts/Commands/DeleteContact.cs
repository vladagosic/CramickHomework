using MediatR;

namespace CramickHomework.Application.Features.Contacts.Commands
{
	public static partial class DeleteContact
	{
		public record Request : IRequest<Unit>
		{
			public Guid Id { get; set; }
		}
	}
}
