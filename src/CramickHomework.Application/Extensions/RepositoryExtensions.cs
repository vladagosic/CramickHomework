using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Domain.Interfaces;
using System.Linq.Expressions;

namespace CramickHomework.Application.Extensions
{
	public static class RepositoryExtensions
	{
		public static IQueryable<TEntity> QueryAllAsNoTracking<TEntity, TId>(
			this IRepository<TEntity, TId> repository,
			params Expression<Func<TEntity, object>>[] paths)
			where TEntity : class, IEntity<TId> =>
			paths.Any()
				? repository.QueryAllIncluding(paths).AsNoTracking()
				: repository.QueryAll().AsNoTracking();

		public static IQueryable<T> QueryAllIncluding<T>(
			this IQueryable<T> query, Func<IQueryable<T>,
			IIncludableQueryable<T, object?>> includes)
			where T : IEntity
		{
			return includes(query);
		}
	}
}
