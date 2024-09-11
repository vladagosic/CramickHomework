namespace CramickHomework.Application.Interfaces
{
	public interface IUnitOfWork
	{
		int SaveChanges();
		Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
		void CancelSaving();
	}
}
