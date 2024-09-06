using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using CramickHomework.Application;
using CramickHomework.Infrastructure.Persistence;

namespace CramickHomework.Infrastructure.Migrations.Seeding
{
    internal class RolesSeed : ISeeding
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<IdentityRole<Guid>>()
                .HasData(CreateRole(Constants.System.AdministratorRoleId, Constants.System.AdministratorRoleName));

            modelBuilder
                .Entity<IdentityRole<Guid>>()
                .HasData(CreateRole(Constants.System.UserRoleId, Constants.System.UserRoleName));
        }

        private IdentityRole<Guid> CreateRole(Guid id, string name)
        {
            var role = new IdentityRole<Guid>(name)
            {
                Id = id,
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            return role;
        }
    }
}
