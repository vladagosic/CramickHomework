namespace CramickHomework.Domain.Interfaces
{
	public interface IEntity
	{ }

	public interface IEntity<TID> : IEntity
	{
		TID Id { get; }
	}
}
