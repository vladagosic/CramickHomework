using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Application.Extensions;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Interfaces;

namespace CramickHomework.Application.Features.Users.Queries
{
	public static partial class GetCurrentUser
	{
		public class QueryHandler : IRequestHandler<Query, UserModel?>
		{
			private readonly IMapper _mapper;
			private readonly IRepository<ApplicationUser, Guid> _repository;
			private readonly ICurrentUserIdProvider _currentUserIdProvider;

			public QueryHandler(
				IMapper mapper,
				IRepository<ApplicationUser, Guid> repository,
				ICurrentUserIdProvider currentUserIdProvider)
			{
				_mapper = mapper;
				_repository = repository;
				_currentUserIdProvider = currentUserIdProvider;
			}

			public async Task<UserModel?> Handle(Query request, CancellationToken cancellationToken)
			{
				return
					await _repository
					.QueryAllAsNoTracking()
					.Where(x => x.Id == _currentUserIdProvider.GetUserId()!)
					.ProjectTo<UserModel?>(_mapper.ConfigurationProvider)
					.FirstOrDefaultAsync(cancellationToken);
			}
		}
	}
}
