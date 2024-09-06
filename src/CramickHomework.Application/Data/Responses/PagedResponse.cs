namespace CramickHomework.Application.Data.Responses
{
	public class PagedResponse<T> : ListResponse<T>
	{
		/// <summary>
		/// Total items count
		/// </summary>
		public int Total { get; set; }

		/// <summary>
		/// Current page
		/// </summary>
		public int Page { get; set; }

		/// <summary>
		/// Page size
		/// </summary>
		public int PageSize { get; set; }
	}
}
