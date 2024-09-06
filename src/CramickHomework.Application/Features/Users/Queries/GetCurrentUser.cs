using AutoMapper;
using MediatR;
using CramickHomework.Application.Features.Users.Domain;

namespace CramickHomework.Application.Features.Users.Queries
{
	public static partial class GetCurrentUser
	{
		public class Query() : IRequest<UserModel>
		{ }

		public class UserModel : GetUsers.UserModel 
		{ }

		public class MappingProfile : Profile
		{
			public MappingProfile()
			{
				CreateMap<ApplicationUser, UserModel>()
					.IncludeBase<ApplicationUser, GetUsers.UserModel>();
			}
		}
	}
}
