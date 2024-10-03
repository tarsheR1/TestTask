namespace WebApplication1.ServerApp.Сore.Interfaces
{
    public interface IUserService
    {
        Task<Guid> SubscribeForEvent(Guid eventId, Guid userId);
        Task<Guid> UnscribeForEvent(Guid eventId, Guid userId);
    }
}