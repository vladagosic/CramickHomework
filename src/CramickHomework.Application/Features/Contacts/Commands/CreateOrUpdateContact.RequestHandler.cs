using AutoMapper;
using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Features.Contacts.Domain;
using CramickHomework.Application.Features.Contacts.Queries;
using CramickHomework.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CramickHomework.Application.Features.Contacts.Commands
{
	public static partial class CreateOrUpdateContact
	{
		public class RequestHandler : IRequestHandler<Request, CreateUpdateResult<GetContacts.ContactModel>>
		{
			private readonly IUnitOfWork _unitOfWork;
			private readonly IRepository<Contact, Guid> _repository;
			private readonly IMapper _mapper;

			public RequestHandler(
				IUnitOfWork unitOfWork,
				IRepository<Contact, Guid> repository,
				IMapper mapper) 
			{
				_unitOfWork = unitOfWork;
				_repository = repository;
				_mapper = mapper;
			}

			public async Task<CreateUpdateResult<GetContacts.ContactModel>> Handle(Request request, CancellationToken cancellationToken)
			{
				Contact? contact = default;

				if (request.Id.HasValue)
				{
					contact = await _repository.QueryAll().FirstOrDefaultAsync(x => x.Id == request.Id) 
						?? throw new ArgumentException($"No Contact with Id: { request.Id }");

					contact.Update(request.Name, request.Phone);

					await _unitOfWork.SaveChangesAsync();

					return 
						CreateUpdateResult<GetContacts.ContactModel>.Updated(
							_mapper.Map<Contact, GetContacts.ContactModel>(contact));
				}
				else
				{
					contact = Contact.Create(request.Name, request.Phone);

					_repository.Add(contact);
					
					await _unitOfWork.SaveChangesAsync();					

					return
						CreateUpdateResult<GetContacts.ContactModel>.Created(
							_mapper.Map<Contact, GetContacts.ContactModel>(contact));
				}
			}
		}
	}
}
