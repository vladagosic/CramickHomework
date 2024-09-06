using Microsoft.EntityFrameworkCore;
using CramickHomework.Domain.Interfaces;
using System.Collections;
using System.Linq.Expressions;

namespace CramickHomework.Infrastructure.Data
{
	public class EntityFrameworkQuery<TEntity, TId> : IQueryable<TEntity> where TEntity : class, IEntity<TId>
	{
		private readonly IQueryable<TEntity> _queryable;

		public EntityFrameworkQuery(DbContext context)
		{
			_queryable = context.Set<TEntity>();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerator<TEntity> GetEnumerator()
		{
			return _queryable.GetEnumerator();
		}

		public Type ElementType => _queryable.ElementType;
		public Expression Expression => _queryable.Expression;
		public IQueryProvider Provider => _queryable.Provider;
	}
}
