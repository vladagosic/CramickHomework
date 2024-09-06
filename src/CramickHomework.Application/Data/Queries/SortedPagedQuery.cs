using CramickHomework.Application.Data.Sorting;

namespace CramickHomework.Application.Data.Queries
{
	public class SortedPagedQuery<TModel> : SimplePagedQuery<TModel>
	{
		public Sort Sort { get; set; } = new();

		public SortedPagedQuery(
			int? pageSize,
			int? pageNumber,
			string? sortField,
			bool descending)
			: base(pageSize, pageNumber)
		{
			Sort = Sort.By(sortField, descending);
		}

		public SortedPagedQuery(
			int? pageSize,
			int? pageNumber,
			string? sortField)
			: base(pageSize, pageNumber)
		{
			if (sortField is null)
			{
				Sort = Sort.Default;
			}
			else
			{
				(string field, bool descending) =
					sortField.StartsWith('!') ? (sortField[1..], true) : (sortField, false);

				Sort = Sort.By(field, descending);
			}
		}
	}
}
