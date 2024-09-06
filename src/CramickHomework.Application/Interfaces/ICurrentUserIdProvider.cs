namespace CramickHomework.Application.Interfaces
{
	public interface ICurrentUserIdProvider
	{
		Guid? GetUserId();
	}
}
