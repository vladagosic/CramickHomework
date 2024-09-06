using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Helpers;
using CramickHomework.Application.Interfaces;
using CramickHomework.Domain.Interfaces;

namespace CramickHomework.Application.Features.Contacts.Domain
{
	public class Contact : IEntity<Guid>, IHasCreatedByUser<Guid>, IHasUpdatedByUser<Guid>
	{
		private Contact() 
		{
			Id = SequentialGuidGenerator.Generate();
		}

		public Guid Id { get; private set; }

		public string Name { get; private set; } = default!;
		public string? Phone { get; set; }

		public DateTimeOffset CreatedOn { get; private set; }
		public Guid CreatedById { get; private set; }

		public DateTimeOffset UpdatedOn { get; private set; }
		public Guid UpdatedById { get; private set; }

		public ApplicationUser CreatedBy { get; private set; } = default!;
		public ApplicationUser UpdatedBy { get; private set; } = default!;

		public void SetCreated(DateTimeOffset createdOn, Guid createdById)
		{
			CreatedOn = createdOn;
			CreatedById = createdById;
		}

		public void SetUpdated(DateTimeOffset updatedOn, Guid updatedById)
		{
			UpdatedById = updatedById;
			UpdatedOn = updatedOn;
		}

		public static Contact Create(string name, string? phone)
		{
			return new Contact
			{
				Name = name,
				Phone = phone
			};
		}

		public void Update(string name, string? phone)
		{
			Name = name;
			Phone = phone;
		}
	}
}
