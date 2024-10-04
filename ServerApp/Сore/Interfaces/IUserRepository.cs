using WebApplication1.Enums_core_;
using WebApplication1.ServerApp.DataAccess.Entities;
using WebApplication1.ServerApp.Сore.Models;

namespace WebApplication1.ServerApp.Сore.Interfaces;

public interface IUserRepository
{
    Task<Guid> Add(UserEntity user);
    Task<UserEntity> GetByEmail(string email);
    Task<HashSet<Permissions>> GetUserPermissions(Guid userId);
    Task<List<UserEntity>> GetUsersForEvent(Guid eventId);
    Task<Guid> SubscribeForEvent(UserEventEntity userEventEntity);
    Task<Guid> UnscribeForEvent(Guid eventId, Guid userId);
}