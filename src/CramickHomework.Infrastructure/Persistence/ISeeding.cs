using Microsoft.EntityFrameworkCore;

namespace CramickHomework.Infrastructure.Persistence
{
	public interface ISeeding
	{
		void Seed(ModelBuilder modelBuilder);
	}
}
