using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Application.Features.Contacts.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CramickHomework.Application.Features.Contacts.Commands
{
	public static partial class DeleteContact
	{
		public class RequestHandler : IRequestHandler<Request, Unit>
		{
			private readonly IRepository<Contact, Guid> _repository;

			public RequestHandler(IRepository<Contact, Guid> repository)
			{
				_repository = repository;
			}

			public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
			{
				var contact = await _repository.QueryAll().FirstOrDefaultAsync(x => x.Id == request.Id)
						?? throw new ArgumentException($"No Contact with Id: {request.Id}");

				_repository.Delete(contact);

				return Unit.Value;
			}
		}
	}
}
