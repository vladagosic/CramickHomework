using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Domain.Interfaces;

namespace CramickHomework.Application.Interfaces
{
	public interface IHasUpdatedByUser<TId> : IHasUpdated
	{
		ApplicationUser UpdatedBy { get; }
	}
}
