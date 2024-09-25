using WebApplication1.Enums_core_;
using WebApplication1.Сore.Models;

namespace WebApplication1.Сore.Interfaces
{
    public interface IUserRepository
    {
        Task<Guid> Add(User user);
        Task<User> GetByEmail(string email);
        Task<HashSet<Permissions>> GetUserPermissions(Guid userId);
        Task<List<User>> GetUsersForEvent(Guid eventId);
        Task<Guid> SubscribeForEvent(Guid eventId, Guid userId);
        Task<Guid> UnscribeForEvent(Guid eventId, Guid userId);
    }
}