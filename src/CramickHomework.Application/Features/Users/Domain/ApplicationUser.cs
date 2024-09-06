using Microsoft.AspNetCore.Identity;
using CramickHomework.Application.Helpers;
using CramickHomework.Domain.Interfaces;

namespace CramickHomework.Application.Features.Users.Domain
{
	public class ApplicationUser : IdentityUser<Guid>, IHasCreatedByUser<Guid>, IEntity<Guid>
	{
        public ApplicationUser() : base() 
		{ 
			Id = SequentialGuidGenerator.Generate();
			SecurityStamp = Guid.NewGuid().ToString();
			UserName = RandomUsernameGenerator.Get();			
		}

        /// <summary>
        /// Gets or sets a full name for the user.
        /// </summary>
        [ProtectedPersonalData]
		public string? FullName { get; set; } = default!;

		public DateTimeOffset CreatedOn { get; private set; }
		public Guid CreatedById { get; private set; } = default!;
		public ApplicationUser CreatedBy { get; } = default!;
		public ICollection<ApplicationUser> UsersCreated { get; private set; } = [];

		public void SetCreated(DateTimeOffset createdOn, Guid createdById)
		{
			CreatedOn = createdOn;
			CreatedById = createdById;
		}
	}
}
