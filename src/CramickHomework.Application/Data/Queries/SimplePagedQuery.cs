namespace CramickHomework.Application.Data.Queries
{
	public class SimplePagedQuery<TModel>
	{
		public const int DefaultPageNumber = 1;
		public const int DefaultPageSize = 10;
		public int PageSize { get; set; } = DefaultPageSize;
		public int PageNumber { get; set; } = DefaultPageNumber;

		public SimplePagedQuery(int? pageSize, int? currentPage)
		{
			PageSize = pageSize ?? DefaultPageSize;
			PageNumber = currentPage ?? DefaultPageNumber;

			if (PageSize <= 0)
				throw new ArgumentOutOfRangeException(nameof(pageSize));

			if (PageNumber <= 0)
				throw new ArgumentOutOfRangeException(nameof(currentPage));
		}
	}
}
