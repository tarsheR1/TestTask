using WebApplication1.Сore.Models;

namespace WebApplication1.Сore.Interfaces
{
    public interface IAdminService
    {
        Task<Guid> SubscribeForEvent(Guid eventId, Guid userId);

        Task<List<User>> GetUsersForEvent(Guid eventId);

        Task<Guid> UnscribeForEvent(Guid eventId, Guid userId);
    }
}