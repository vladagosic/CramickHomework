using CramickHomework.Application;
using CramickHomework.Application.Features.Contacts.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CramickHomework.Infrastructure.Persistence.Configuration
{
	public class ContactConfiguration : IEntityTypeConfiguration<Contact>
	{
		public void Configure(EntityTypeBuilder<Contact> builder)
		{
			builder
				.Property(e => e.Id)
				.IsRequired()
				.HasMaxLength(36)
				.HasConversion(
					x => x.ToString(),
					x => Guid.Parse(x));

			builder
				.Property(x => x.Name)
				.IsRequired()
				.HasMaxLength(Constants.User.FullNameLength);

			builder
				.Property(x => x.Phone)
				.HasMaxLength(Constants.User.PhoneLength);

			builder
				.Property(x => x.CreatedById)
				.IsRequired()
				.HasConversion(
					x => x.ToString(),
					x => Guid.Parse(x));

			builder
				.Property(x => x.UpdatedById)
				.IsRequired()
				.HasConversion(
					x => x.ToString(),
					x => Guid.Parse(x));

			builder
				.HasOne(x => x.CreatedBy)
				.WithMany(x => x.ContactsCreated)
				.HasForeignKey(x => x.CreatedById)
				.OnDelete(DeleteBehavior.Restrict);

			builder
				.HasOne(x => x.UpdatedBy)
				.WithMany(x => x.ContactsUpdated)
				.HasForeignKey(x => x.UpdatedById)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
