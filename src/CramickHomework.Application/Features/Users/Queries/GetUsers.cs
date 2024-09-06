using AutoMapper;
using MediatR;
using CramickHomework.Application.Data.Queries;
using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Features.Users.Domain;

namespace CramickHomework.Application.Features.Users.Queries
{
	public static partial class GetUsers
	{
		public class Query
			: SortedPagedQuery<UserModel>,
			  IRequest<PagedResponse<UserModel>>
		{
			private Query(int? pageSize, int? pageNumber, string? sort)
				: base(pageSize, pageNumber, sort)
			{ }

			public static Query Create(int? pageSize, int? pageNumber, string? sort)
			{
				return new Query(pageSize, pageNumber, sort);
			}
		}

		public class UserModel
		{
			public Guid Id { get; set; }
			public string? FullName { get; set; }
			public string? Email { get; set; }
			public bool EmailConfirmed { get; set; }
			public string? CreatedBy { get; set; }
			public DateTimeOffset? CreatedOn { get; set; }
		}

		public class MappingProfile : Profile
		{
			public MappingProfile()
			{
				CreateMap<ApplicationUser, UserModel>()
					.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.FullName));
			}
		}
	}
}
