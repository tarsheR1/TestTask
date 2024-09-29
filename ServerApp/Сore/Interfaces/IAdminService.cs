using WebApplication1.ServerApp.Сore.Models;

namespace WebApplication1.ServerApp.Сore.Interfaces
{
    public interface IAdminService
    {
        Task<Guid> SubscribeForEvent(Guid eventId, Guid userId);

        Task<List<User>> GetUsersForEvent(Guid eventId);

        Task<Guid> UnscribeForEvent(Guid eventId, Guid userId);
    }
}