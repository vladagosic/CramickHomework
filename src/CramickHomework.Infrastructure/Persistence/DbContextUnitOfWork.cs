using CramickHomework.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CramickHomework.Infrastructure.Persistence
{
	public class DbContextUnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
	{
		private readonly TDbContext _context;
		private readonly ILogger<DbContextUnitOfWork<TDbContext>> _logger;
		private bool _cancelSaving;

		public DbContextUnitOfWork(TDbContext context, ILogger<DbContextUnitOfWork<TDbContext>> logger)
		{
			_context = context;
			_logger = logger;
		}

		public int SaveChanges()
		{
			Task<int> task = Task.Run(async () => await SaveChangesAsync());
			return task.Result;
		}

		public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			if (_cancelSaving)
			{
				_logger.LogWarning("Not saving database changes since saving was canceled.");
				return 0;
			}

			int numberOfChanges = await _context.SaveChangesAsync(cancellationToken);
			_logger.LogDebug($"{numberOfChanges} changes were saved to database {_context.Database.GetDbConnection().Database}");

			return numberOfChanges;
		}

		public void CancelSaving()
		{
			_cancelSaving = true;
		}
	}
}
