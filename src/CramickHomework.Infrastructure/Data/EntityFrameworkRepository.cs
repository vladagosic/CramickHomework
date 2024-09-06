using Microsoft.EntityFrameworkCore;
using CramickHomework.Application.Data.Interfaces;
using CramickHomework.Domain.Interfaces;
using System.Linq.Expressions;

namespace CramickHomework.Infrastructure.Data
{
	public class EntityFrameworkRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class, IEntity<TId>
	{
		public EntityFrameworkRepository(DbContext context)
		{
			DbContext = context;
			DbSet = context.Set<TEntity>();
		}

		protected DbSet<TEntity> DbSet { get; }

		protected DbContext DbContext { get; }

		public IQueryable<TEntity> QueryAll()
		{
			return DbSet;
		}

		public IQueryable<TEntity> QueryAllIncluding(params Expression<Func<TEntity, object>>[] includes)
		{
			if (includes == null)
			{
				throw new ArgumentNullException(nameof(includes));
			}

			return
				includes.Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>(
					DbSet, (current, includeProperty) => current.Include(includeProperty));
		}

		public void Add(TEntity item)
		{
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			DbSet.Add(item);
		}

		public void Delete(TEntity item)
		{
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			DbSet.Remove(item);
		}

		public void Delete(TId id)
		{
			var item = FindById(id);
			if (item != null)
			{
				Delete(item);
			}
		}

		public TEntity? FindById(TId id)
		{
			return DbSet.Find(id);
		}

		public async Task<TEntity?> FindByIdAsync(TId id)
		{
			return await DbSet.FindAsync(id);
		}

		public void DeleteRange(IEnumerable<TEntity> entities)
		{
			DbSet.RemoveRange(entities);
		}

		public IQueryable<TEntity> ExecuteRawSql(string sqlQuery)
		{
			return DbSet.FromSqlRaw(sqlQuery);
		}
	}
}
