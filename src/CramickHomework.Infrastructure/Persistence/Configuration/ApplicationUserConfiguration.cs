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
				.Property(e => e.Id)
				.IsRequired()
				.HasMaxLength(36)
				.HasConversion(
					x => x.ToString(),
					x => Guid.Parse(x));

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
				.HasConversion(
					x => x.ToString(),
					x => Guid.Parse(x));

			builder
				.HasMany(x => x.UsersCreated)
				.WithOne(x => x.CreatedBy)
				.HasForeignKey(x => x.CreatedById)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
