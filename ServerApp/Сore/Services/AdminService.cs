using WebApplication1.Authorization;
using WebApplication1.Contracts;
using WebApplication1.DataAccess.Repositories;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Сore.Services
{
    public class AdminService : IAdminService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IUserRepository _usersRepository;

        public AdminService(IEventsRepository eventsRepository, IUserRepository userRepository)
        {
            _eventsRepository = eventsRepository;
            _usersRepository = userRepository;
        }

        public async Task<List<User>> GetUsersForEvent(Guid eventId)
        {
            var users = await _usersRepository.GetUsersForEvent(eventId);

            return users;
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
