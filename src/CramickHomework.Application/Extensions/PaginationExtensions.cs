using AutoMapper;
using AutoMapper.QueryableExtensions;
using CramickHomework.Application.Data.Queries;
using CramickHomework.Application.Data.Responses;
using CramickHomework.Application.Data.Sorting;
using CramickHomework.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CramickHomework.Application.Extensions
{
	public static class PaginationExtensions
	{
		public static async Task<PagedResponse<TModel>> ExecutePagedQuery<TEntity, TModel>(
			this IQueryable<TEntity> query,
			SortedPagedQuery<TModel> request,
			IMapper mapper,
			CancellationToken cancellationToken)
			where TEntity : IEntity
		{
			return
			await query
				.ExecutePagedQuery<TEntity, TModel>(
					request.PageNumber,
					request.PageSize,
					request.Sort,
					mapper,
					cancellationToken);
		}

		public static async Task<PagedResponse<TModel>> ExecutePagedQuery<TEntity, TModel>(
			this IQueryable<TEntity> query,
			SimplePagedQuery<TModel> request,
			IMapper mapper,
			CancellationToken cancellationToken)
			where TEntity : IEntity
		{
			return
				await query
				.ExecutePagedQuery<TEntity, TModel>(
					request.PageNumber,
					request.PageSize,
					Sort.Default,
					mapper,
					cancellationToken);
		}

		public static async Task<PagedResponse<TModel>> ExecutePagedQuery<TEntity, TModel>(
			this IQueryable<TEntity> query,
			int page,
			int pageSize,
			Sort sort,
			IMapper mapper,
			CancellationToken cancellationToken)
			where TEntity : IEntity
		{
			return
			await query
				.SortBy(sort)
				.ProjectTo<TModel>(mapper.ConfigurationProvider)
				.ToPagedResponse(page, pageSize, cancellationToken);
		}

		public static async Task<PagedResponse<TEntity>> ExecutePagedQuery<TEntity>(
			this IQueryable<TEntity> query,
			SortedPagedQuery<TEntity> request,
			CancellationToken cancellationToken)
			where TEntity : IEntity
		{
			return
				await query
				.ExecutePagedQuery(
					request.PageNumber,
					request.PageSize,
					request.Sort,
					cancellationToken);
		}

		public static async Task<PagedResponse<TEntity>> ExecutePagedQuery<TEntity>(
			this IQueryable<TEntity> query,
			SimplePagedQuery<TEntity> request,
			CancellationToken cancellationToken)
			where TEntity : IEntity
		{
			return
				await query
				.ExecutePagedQuery(
					request.PageNumber,
					request.PageSize,
					Sort.Default,
					cancellationToken);
		}

		public static async Task<PagedResponse<TEntity>> ExecutePagedQuery<TEntity>(
			this IQueryable<TEntity> query,
			int page,
			int pageSize,
			Sort sort,
			CancellationToken cancellationToken)
			where TEntity : IEntity
		{
			return
				await query
				.SortBy(sort)
				.ToPagedResponse(page, pageSize, cancellationToken);
		}

		public static async Task<PagedResponse<T>> ToPagedResponse<T>(
			this IQueryable<T> query,
			int currentPage,
			int pageSize,
			CancellationToken cancellationToken = default)
		{
			var totalCount = await query.CountAsync(cancellationToken);

			var pagedItems = await query
				.Paginate(currentPage, pageSize)
				.ToListAsync(cancellationToken);

			return new PagedResponse<T>
			{
				Items = pagedItems ?? Enumerable.Empty<T>(),
				Total = totalCount,
				Page = currentPage,
				PageSize = pageSize
			};
		}

		public static IQueryable<T> SortBy<T>(
			this IQueryable<T> query,
			Sort sort)
		{
			return query.SortBy(sort.Field, sort.Descending);
		}

		public static IQueryable<T> SortBy<T>(
			this IQueryable<T> query,
			string sortField,
			bool sortDescending = false)
		{
			if (string.IsNullOrEmpty(sortField))
				return query;

			var propertyInfo = typeof(T)
				.GetProperties()
				.FirstOrDefault(x => string.Equals(x.Name, sortField, StringComparison.InvariantCultureIgnoreCase));

			return propertyInfo == null ? query : query.OrderByProperty(propertyInfo.Name, sortDescending);
			//TODO: Throw some bad request exception that prop is not defined on entity
		}

		private static IQueryable<T> Paginate<T>(this IQueryable<T> query, int currentPage, int pageSize)
		{
			return query
				.Skip((currentPage - 1) * pageSize)
				.Take(pageSize);
		}
	}
}
