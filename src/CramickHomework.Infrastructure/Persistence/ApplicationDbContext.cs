using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CramickHomework.Application;
using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Application.Helpers;
using CramickHomework.Application.Interfaces;
using CramickHomework.Domain.Interfaces;
using CramickHomework.Infrastructure.Extensions;

namespace CramickHomework.Infrastructure.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
	{
		private readonly ICurrentUserIdProvider _currentUserIdProvider;
		private readonly ILogger<ApplicationDbContext> _logger;

		public ApplicationDbContext(
			DbContextOptions options,
			ICurrentUserIdProvider currentUserIdProvider,
			ILogger<ApplicationDbContext> logger)
			: base(options)
		{
			_currentUserIdProvider = currentUserIdProvider;
			_logger = logger;
		}

		public Action<ModelBuilder>? ModelBuilderConfigurator { private get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			ModelBuilderConfigurator?.Invoke(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(AssemblyFinder.InfrastructureAssembly);

			base.OnModelCreating(modelBuilder);
		}

		public override int SaveChanges()
		{
			var task = Task.Run(async () => await SaveChangesAsync());
			return task.GetAwaiter().GetResult();
		}

		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			PopulateCreatedBy();

			var saved = await base.SaveChangesAsync(cancellationToken);

			return saved;
		}

		private void PopulateCreatedBy()
		{
			Guid userId = _currentUserIdProvider.GetUserId() ?? Constants.System.UserId;

			ChangeTracker
				.GetAddedEntitiesWithCreated()
				.ForEach(entity =>
				{
					var createdEntity = (IHasCreated)entity.Entity;
					createdEntity.SetCreated(DateTimeOffset.Now, userId);
				});
		}
	}
}
