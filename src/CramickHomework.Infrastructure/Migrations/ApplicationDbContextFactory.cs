using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using CramickHomework.Infrastructure.Migrations.Seeding;
using CramickHomework.Infrastructure.Persistence;
using CramickHomework.Infrastructure.Persistence.Configuration;
using CramickHomework.Infrastructure.Providers;

namespace CramickHomework.Infrastructure.Migrations
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			return CreateDbContext(ReadConnectionString());
		}

		private static ApplicationDbContext CreateDbContext(string connectionString)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseMySQL(connectionString);

			return 
				new ApplicationDbContext(
					optionsBuilder.Options, 
					new NullUserIdProvider(), 
					new NullLogger<ApplicationDbContext>())
				{
					ModelBuilderConfigurator = DbSeedDataInitializer.SeedData
				};
		}

		private static string ReadConnectionString()
		{
			const string environmentVariableName = "DbConnectionString";
			string? connectionString = Environment.GetEnvironmentVariable(environmentVariableName);

			if (string.IsNullOrEmpty(connectionString))
			{
				var configuration = JsonConfigurationLoader.GetConfiguration();

				return configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Invalid path to appsettings.json");
			}

			return connectionString;
		}
	}
}
