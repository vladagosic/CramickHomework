namespace CramickHomework.Application.Data.Responses
{
	public class ListResponse<T>
	{
		/// <summary>
		/// List of Items
		/// </summary>
		public IEnumerable<T> Items { get; set; } = [];
	}
}
