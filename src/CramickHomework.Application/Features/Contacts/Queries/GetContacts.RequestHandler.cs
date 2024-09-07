using AutoMapper;
using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Extensions;
using CramickHomework.Application.Features.Contacts.Domain;
using MediatR;

namespace CramickHomework.Application.Features.Contacts.Queries
{
	public static partial class GetContacts
	{
		public class QueryHandler : IRequestHandler<Query, PagedResponse<ContactModel>>
		{
			private readonly IMapper _mapper;
			private readonly IRepository<Contact, Guid> _repository;

			public QueryHandler(
				IMapper mapper,
				IRepository<Contact, Guid> repository)
			{
				_mapper = mapper;
				_repository = repository;
			}

			public async Task<PagedResponse<ContactModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				return
					await _repository
					.QueryAllAsNoTracking()
					.ExecutePagedQuery(request, _mapper, cancellationToken);
			}
		}
	}
}
