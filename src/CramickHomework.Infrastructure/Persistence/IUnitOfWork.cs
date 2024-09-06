namespace CramickHomework.Infrastructure.Persistence
{
	public interface IUnitOfWork
	{
		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		void CancelSaving();
	}
}
