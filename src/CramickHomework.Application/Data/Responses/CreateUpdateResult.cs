namespace CramickHomework.Application.Data.Responses
{
	public class CreateUpdateResult<TItem>
	{
		public TItem Item { get; private set; } = default!;
		public bool IsCreated { get; private set; }

		public static CreateUpdateResult<T> Created<T>(T item)
		{
			return new CreateUpdateResult<T> { Item = item, IsCreated = true };
		}

		public static CreateUpdateResult<T> Updated<T>(T item)
		{
			return new CreateUpdateResult<T> { Item = item, IsCreated = false };
		}
	}
}
