using Autofac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Application.Extensions;
using CramickHomework.Infrastructure.Data;
using CramickHomework.Infrastructure.Persistence;
using CramickHomework.Infrastructure.Persistence.Options;
using System.Reflection;
using Module = Autofac.Module;

namespace CramickHomework.Infrastructure.Autofac
{
	public class EntityFrameworkModule<TDbContext> : Module where TDbContext : DbContext
	{
		public Assembly? MigrationsAssembly { private get; set; }

		protected override void Load(ContainerBuilder builder)
		{
			if (builder == null)
			{
				throw new ArgumentNullException(nameof(builder));
			}

			builder.RegisterGeneric(typeof(EntityFrameworkRepository<,>))
				.As(typeof(IRepository<,>))
				.InstancePerLifetimeScope();

			builder.RegisterGeneric(typeof(EntityFrameworkQuery<,>))
				.As(typeof(IQueryable<>))
				.InstancePerLifetimeScope();

			builder.RegisterAssemblyTypes(typeof(IRepository<,>).Assembly)
				.Where(type => typeof(IRepository<,>).ImplementsInterface(type) || type.Name.EndsWith("Repository"))
				.AsImplementedInterfaces()
				.InstancePerLifetimeScope();

			builder.Register(CreateDbContextOptions).As<DbContextOptions>().SingleInstance();
			builder.RegisterType<TDbContext>().AsSelf().As<DbContext>().InstancePerLifetimeScope();
			builder.RegisterType<DbContextUnitOfWork<TDbContext>>().As<IUnitOfWork>().InstancePerLifetimeScope();
		}

		private DbContextOptions CreateDbContextOptions(IComponentContext container)
		{
			var loggerFactory = container.Resolve<ILoggerFactory>();
			var configuration = container.Resolve<IConfiguration>();
			var dbContextSettings = container.Resolve<DbContextSettings>();

			var optionsBuilder = new DbContextOptionsBuilder();

			optionsBuilder
				.UseLoggerFactory(loggerFactory)
				.EnableSensitiveDataLogging(dbContextSettings.SensitiveDataLoggingEnabled);

			optionsBuilder.UseSqlServer(
				configuration.GetConnectionString("DefaultConnection"),
				sqlOptions => SetupSqlOptions(sqlOptions, dbContextSettings));

			return optionsBuilder.Options;
		}

		private SqlServerDbContextOptionsBuilder SetupSqlOptions(
			SqlServerDbContextOptionsBuilder sqlOptions,
			DbContextSettings dbContextSettings)
		{

			//Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency
			sqlOptions = sqlOptions.EnableRetryOnFailure(
				dbContextSettings.ConnectionResiliencyMaxRetryCount,
				dbContextSettings.ConnectionResiliencyMaxRetryDelay,
				null);

			if (MigrationsAssembly != null)
			{
				sqlOptions = sqlOptions.MigrationsAssembly(MigrationsAssembly.FullName);
			}

			return sqlOptions;
		}
	}
}
