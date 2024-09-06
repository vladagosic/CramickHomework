using CramickHomework.Domain.Interfaces;
using System.Linq.Expressions;

namespace CramickHomework.Application.Data.Interfaces
{
	public interface IRepository<TEntity, TId> where TEntity : IEntity<TId>
	{
		void Add(TEntity item);

		void Delete(TEntity item);

		void DeleteRange(IEnumerable<TEntity> entities);

		IQueryable<TEntity> QueryAll();

		IQueryable<TEntity> QueryAllIncluding(params Expression<Func<TEntity, object>>[] includes);

		void Delete(TId id);

		TEntity? FindById(TId id);

		Task<TEntity?> FindByIdAsync(TId id);

		IQueryable<TEntity> ExecuteRawSql(string sqlQuery);
	}
}
