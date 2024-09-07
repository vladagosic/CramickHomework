using AutoMapper;
using CramickHomework.Application.Data.Queries;
using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Features.Contacts.Domain;
using MediatR;

namespace CramickHomework.Application.Features.Contacts.Queries
{
	public static partial class GetContacts
	{
		public class Query
			: SortedPagedQuery<ContactModel>,
			  IRequest<PagedResponse<ContactModel>>
		{
			private Query(int? pageSize, int? pageNumber, string? sort)
				: base(pageSize, pageNumber, sort)
			{ }

			public static Query Create(int? pageSize, int? pageNumber, string? sort)
			{
				return new Query(pageSize, pageNumber, sort);
			}
		}

		public record ContactModel
		{
			public Guid Id { get; set; }
			public string Name { get; set; } = default!;
			public string? Phone { get; set; }
			public string? CreatedBy { get; set; }
			public DateTimeOffset? CreatedOn { get; set; }
			public string? UpdatedBy { get; set; }
			public DateTimeOffset? UpdatedOn { get; set; }
		}

		public class MappingProfile : Profile
		{
			public MappingProfile()
			{
				CreateMap<Contact, ContactModel>()
					.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy.FullName))
					.ForMember(dest => dest.UpdatedBy, opt => opt.MapFrom(src => src.UpdatedBy.FullName));
			}
		}
	}
}
