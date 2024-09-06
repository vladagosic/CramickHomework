using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CramickHomework.Domain.Interfaces;

namespace CramickHomework.Infrastructure.Extensions
{
	public static class ChangeTrackerExtensions
	{
		public static List<EntityEntry<IEntity>> GetAddedEntitiesWithCreated(this ChangeTracker changeTracker) =>
			changeTracker
				.GetAddedEntitiesWhichImplement<IHasCreated>()
				.ToList();

		private static List<EntityEntry<IEntity>> GetAddedEntitiesWhichImplement<T>(this ChangeTracker changeTracker) =>
		   changeTracker
			   .Entries<IEntity>()
			   .Where(x => (x.State == EntityState.Added) && x.Entity is T)
			   .ToList();
	}
}
