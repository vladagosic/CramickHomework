using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CramickHomework.Application;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Helpers;
using CramickHomework.Infrastructure.Persistence;
using CramickHomework.Infrastructure.Persistence.Configuration;

namespace CramickHomework.Infrastructure.Migrations.Seeding
{
    internal class SystemAndAdminUsersSeed : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            var configuration = JsonConfigurationLoader.GetConfiguration();

            modelBuilder
                .Entity<ApplicationUser>()
                .HasData(
                    CreateUser(
                        Constants.System.UserId,
                        Constants.System.Email,
                        Guid.NewGuid().ToString(),
                        Constants.System.Name));

            modelBuilder
                .Entity<ApplicationUser>()
                .HasData(
                    CreateUser(
                        Guid.Parse(configuration["Identity:Administrator:Id"]!),
                        configuration["Identity:Administrator:Email"]!,
                        configuration["Identity:Administrator:Password"]!,
                        configuration["Identity:Administrator:Name"]!));

            modelBuilder
                .Entity<IdentityUserRole<Guid>>()
                .HasData(
                    new IdentityUserRole<Guid>()
                    {
                        UserId = Constants.System.UserId,
                        RoleId = Constants.System.AdministratorRoleId
                    });

            modelBuilder
                .Entity<IdentityUserRole<Guid>>()
                .HasData(
                    new IdentityUserRole<Guid>()
                    {
                        UserId = Guid.Parse(configuration["Identity:Administrator:Id"]!),
                        RoleId = Constants.System.AdministratorRoleId
                    });
        }

        private ApplicationUser CreateUser(Guid id, string email, string password, string fullName)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var username = RandomUsernameGenerator.Get();
            var user = new ApplicationUser
            {
                Id = id,
                Email = email,
                NormalizedEmail = email.ToUpper(),
                UserName = username,
                NormalizedUserName = username.ToUpper(),
                FullName = fullName,
                EmailConfirmed = true
            };

            user.SetCreated(DateTimeOffset.Now, Constants.System.UserId);

            user.PasswordHash = hasher.HashPassword(user, password);

            return user;
        }
    }
}
