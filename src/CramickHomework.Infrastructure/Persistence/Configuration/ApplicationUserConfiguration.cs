using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CramickHomework.Application;
using CramickHomework.Application.Features.Users.Domain;

namespace CramickHomework.Infrastructure.Persistence.Configuration
{
	internal class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder
				.Property(x => x.FullName)
				.IsRequired()
				.HasMaxLength(Constants.User.FullNameLength);

			builder
				.Property(x => x.Email)
				.IsRequired()
				.HasMaxLength(Constants.User.EmailLength);

			builder
				.Property(x => x.CreatedById)
				.IsRequired()
				.HasMaxLength(450);

			builder
				.HasMany(x => x.UsersCreated)
				.WithOne(x => x.CreatedBy)
				.HasForeignKey(x => x.CreatedById)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
