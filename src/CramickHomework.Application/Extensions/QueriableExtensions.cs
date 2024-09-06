using System.Linq.Expressions;

namespace CramickHomework.Application.Extensions
{
	public static class QueriableExtensions
	{
		public static IQueryable<T> OrderByProperty<T>(this IQueryable<T> query, string property, bool sortDescending = false)
		{
			var type = typeof(T);
			var prop =
				type.GetProperty(property);

			if (prop == null)
			{
				return query;
			}

			var method = sortDescending ? "OrderByDescending" : "OrderBy";
			var parameter = Expression.Parameter(type, "p");
			var propertyAccess = Expression.MakeMemberAccess(parameter, prop);
			var orderByExpression = Expression.Lambda(propertyAccess, parameter);

			var resultExpression =
				Expression.Call(
					typeof(Queryable),
					method,
					new[] { type, prop.PropertyType },
					query.Expression,
					Expression.Quote(orderByExpression));

			return query.Provider.CreateQuery<T>(resultExpression);
		}
	}
}
