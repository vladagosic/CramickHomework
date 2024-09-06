using CramickHomework.Application.Interfaces;

namespace CramickHomework.Infrastructure.Providers
{
	internal class NullUserIdProvider : ICurrentUserIdProvider
	{
		public Guid? GetUserId() => null;
	}
}
