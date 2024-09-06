using CramickHomework.Domain.Interfaces;

namespace CramickHomework.Application.Features.Users.Domain
{
	public interface IHasCreatedByUser<TId> : IHasCreated
	{
		ApplicationUser CreatedBy { get; }
	}
}
