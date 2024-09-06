using CramickHomework.Application.Features.Users.Domain;
using CramickHomework.Domain.Interfaces;

namespace CramickHomework.Application.Interfaces
{
    public interface IHasCreatedByUser<TId> : IHasCreated
    {
        ApplicationUser CreatedBy { get; }
    }
}
