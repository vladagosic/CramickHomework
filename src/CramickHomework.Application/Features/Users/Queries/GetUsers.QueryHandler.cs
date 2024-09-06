using AutoMapper;
using MediatR;
using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Extensions;
using CramickHomework.Application.Features.Users.Domain;

namespace CramickHomework.Application.Features.Users.Queries
{
	public static partial class GetUsers
	{
		public class QueryHandler : IRequestHandler<Query, PagedResponse<UserModel>>
		{
			private readonly IMapper _mapper;
			private readonly IRepository<ApplicationUser, Guid> _repository;

			public QueryHandler(
				IMapper mapper,
				IRepository<ApplicationUser, Guid> repository)
			{
				_mapper = mapper;
				_repository = repository;
			}

			public async Task<PagedResponse<UserModel>> Handle(Query request, CancellationToken cancellationToken)
			{
				return
					await _repository
					.QueryAllAsNoTracking()
					.ExecutePagedQuery(request, _mapper, cancellationToken);
			}
		}
	}
}
