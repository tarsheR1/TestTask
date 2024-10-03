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
            return await _usersRepository.SubscribeForEvent(eventId, userId);
        }

        public async Task<Guid> UnscribeForEvent(Guid eventId, Guid userId)
        {
            await _usersRepository.UnscribeForEvent(eventId, userId);

            return userId;
        }
    }
}
