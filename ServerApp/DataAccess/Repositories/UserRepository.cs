using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApplication1.DataAccess.Entities;
using WebApplication1.Enums_core_;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.Сore.Interfaces;
using WebApplication1.Сore.Models;

namespace WebApplication1.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventsDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(EventsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Guid> Add(User user)
        {
            var userEntity = new UserEntity
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                RegistrationDate = user.RegistrationDate,

                HashPassword = user.PasswordHash
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            if (userEntity == null)
            {
                throw new Exception("This email not registred");
            }

            return _mapper.Map<User>(userEntity);
        }

        public async Task<HashSet<Permissions>> GetUserPermissions(Guid userId)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToArrayAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permissions)p.Id)
                .ToHashSet();
        }

        public async Task<List<User>> GetUsersForEvent(Guid eventId)
        {
            {
                var userEntities = await _context.Users
                    .Join(
                        _context.EventParticipants,
                        user => user.Id,
                        eventParticipant => eventParticipant.UserId,
                        (user, eventParticipant) => new { User = user, EventParticipant = eventParticipant })
                    .Join(
                        _context.Events,
                        combined => combined.EventParticipant.EventId,
                        evt => evt.Id,
                        (combined, evt) => new { combined.User, evt })
                    .Where(joined => joined.evt.Id == eventId) 
                    .Select(joined => joined.User)
                    .ToListAsync();


                List<User> users = _mapper.Map<List<UserEntity>, List<User>>( userEntities);
                return users;
            }
        }

        public async Task<Guid> SubscribeForEvent(Guid eventId, Guid userId)
        {
            {

                var newEventParticipate = new UserEventEntity
                {
                    EventId = eventId,
                    UserId = userId
                };

                _context.EventParticipants.Add(newEventParticipate);
                await _context.SaveChangesAsync();
                return eventId;
            }
        }

        public async Task<Guid> UnscribeForEvent(Guid eventId, Guid userId)
        {
            {
                var eventParticipate = _context.EventParticipants.FirstOrDefault(ep => ep.EventId == eventId && ep.UserId == userId);

                if (eventParticipate != null)
                {
                    // Remove the found EventParticipate record and save changes
                    _context.EventParticipants.Remove(eventParticipate);
                    _context.SaveChanges();
                }

                return userId;
            }
        }

    }
}
