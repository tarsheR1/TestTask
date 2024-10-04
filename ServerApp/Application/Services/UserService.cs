using WebApplication1.ServerApp.DataAccess.Entities;
using WebApplication1.ServerApp.Сore.Interfaces;

namespace WebApplication1.ServerApp.Application.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _usersRepository;

        public UserService(IUserRepository userRepository)
        {
            _usersRepository = userRepository;
        }

        public async Task<Guid> SubscribeForEvent(Guid eventId, Guid userId)
        {
            var newEventParticipate = new UserEventEntity
            {
                EventId = eventId,
                UserId = userId
            };
            return await _usersRepository.SubscribeForEvent(newEventParticipate);
        }

        public async Task<Guid> UnscribeForEvent(Guid eventId, Guid userId)
        {
            await _usersRepository.UnscribeForEvent(eventId, userId);

            return userId;
        }
    }
}
