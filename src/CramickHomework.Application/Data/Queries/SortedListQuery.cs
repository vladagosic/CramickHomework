using CramickHomework.Application.Data.Sorting;

namespace CramickHomework.Application.Data.Queries
{
	public class SortedListQuery<TModel>(string? sortField, bool descending = false)
	{
		public Sort Sort { get; set; } = Sort.By(sortField, descending);
	}
}
