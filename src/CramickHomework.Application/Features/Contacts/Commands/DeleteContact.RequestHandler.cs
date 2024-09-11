using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Application.Features.Contacts.Domain;
using CramickHomework.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CramickHomework.Application.Features.Contacts.Commands
{
	public static partial class DeleteContact
	{
		public class RequestHandler : IRequestHandler<Request, Unit>
		{
			private readonly IUnitOfWork _unitOfWork;
			private readonly IRepository<Contact, Guid> _repository;

			public RequestHandler(
				IUnitOfWork unitOfWork,
				IRepository<Contact, Guid> repository)
			{
				_unitOfWork = unitOfWork;
				_repository = repository;
			}

			public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
			{
				var contact = await _repository.QueryAll().FirstOrDefaultAsync(x => x.Id == request.Id)
						?? throw new ArgumentException($"No Contact with Id: {request.Id}");

				_repository.Delete(contact);

				await _unitOfWork.SaveChangesAsync();

				return Unit.Value;
			}
		}
	}
}
