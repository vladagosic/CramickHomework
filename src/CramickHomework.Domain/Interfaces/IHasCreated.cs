namespace CramickHomework.Domain.Interfaces
{
	public interface IHasCreated
	{
		DateTimeOffset CreatedOn { get; }
		Guid CreatedById { get; }

		void SetCreated(DateTimeOffset createdOn, Guid createdById);
	}
}
