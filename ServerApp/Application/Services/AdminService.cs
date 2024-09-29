using WebApplication1.ServerApp.Infrastructure.Authorization;
using WebApplication1.ServerApp.Сore.Contracts;
using WebApplication1.ServerApp.DataAccess.Repositories;
using WebApplication1.ServerApp.Сore.Models;
using WebApplication1.ServerApp.Сore.Interfaces;
using AutoMapper;
using WebApplication1.ServerApp.DataAccess.Entities;

namespace WebApplication1.ServerApp.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly IUserRepository _usersRepository;
        private readonly Mapper _mapper;

        public AdminService(
            IEventsRepository eventsRepository, 
            IUserRepository userRepository,
            Mapper mapper)
        {
            _eventsRepository = eventsRepository;
            _usersRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<User>> GetUsersForEvent(Guid eventId)
        {
            var userEntities = await _usersRepository.GetUsersForEvent(eventId);
            List<User> users = _mapper.Map<List<UserEntity>, List<User>>(userEntities);
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
