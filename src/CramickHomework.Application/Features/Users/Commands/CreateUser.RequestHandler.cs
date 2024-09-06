using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Extensions;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Features.Users.Queries;
using CramickHomework.Application.Interfaces;

namespace CramickHomework.Application.Features.Users.Commands
{
	public static partial class CreateUser
	{
		public class RequestHandler : IRequestHandler<Request, CreateUpdateResult<GetUsers.UserModel>>
		{
			private readonly UserManager<ApplicationUser> _userManager;
			private readonly IRepository<ApplicationUser, Guid> _userRepository;
			private readonly ICurrentUserIdProvider _currentUserIdProvider;
			private readonly IMapper _mapper;

			public RequestHandler(
				UserManager<ApplicationUser> userManager,
				IRepository<ApplicationUser, Guid> userRepository,
				ICurrentUserIdProvider currentUserIdProvider,
				IMapper mapper)
			{
				_userManager = userManager;
				_userRepository = userRepository;
				_currentUserIdProvider = currentUserIdProvider;
				_mapper = mapper;
			}

			public async Task<CreateUpdateResult<GetUsers.UserModel>> Handle(Request request, CancellationToken cancellationToken)
			{
				//if (!await IsCurrentUserAdministrator())
				//{
				//	throw new InvalidOperationException($"Only users in {Constants.System.AdministratorRoleName} can create other users.");
				//}

				var user =
					new ApplicationUser
					{
						FullName = request.FullName,
						Email = request.Email,

					};

				var result = await _userManager.CreateAsync(user, request.Password!);

				if (!result.Succeeded)
				{
					throw new ArgumentException($"Unable to register user {request.Email} errors: {result.GetErrorsText()}");
				}

				var created =
					await _userRepository
					.QueryAllAsNoTracking()
					.Where(x => x.Email == request.Email)
					.ProjectTo<GetUsers.UserModel>(_mapper.ConfigurationProvider)
					.SingleAsync(cancellationToken);

				return CreateUpdateResult<GetUsers.UserModel>.Created(created);
			}

			private async Task<bool> IsCurrentUserAdministrator()
			{
				var user = await _userManager.FindByIdAsync(_currentUserIdProvider.GetUserId()?.ToString()!);

				return
					user is not null &&
					await _userManager.IsInRoleAsync(user, Constants.System.AdministratorRoleName);

			}
		}
	}
}
