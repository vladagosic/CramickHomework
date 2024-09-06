namespace CramickHomework.Domain.Interfaces
{
	public interface IHasUpdated
	{
		DateTimeOffset UpdatedOn { get; }
		Guid UpdatedById { get; }

		void SetUpdated(DateTimeOffset updatedOn, Guid updatedById);
	}
}
