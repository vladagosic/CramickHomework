using Microsoft.EntityFrameworkCore;
using CramickHomework.Infrastructure.Persistence;

namespace CramickHomework.Infrastructure.Migrations.Seeding
{
	public static class DbSeedDataInitializer
    {
        private static readonly IList<ISeeding> Seedings = new List<ISeeding>();

        static DbSeedDataInitializer()
        {
            Seedings.Add(new RolesSeed());
            Seedings.Add(new SystemAndAdminUsersSeed());
        }

        // EF Core way of seeding data: https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
        public static void SeedData(ModelBuilder modelBuilder)
        {
            foreach (ISeeding seeding in Seedings)
            {
                seeding.Seed(modelBuilder);
            }
        }
    }
}
