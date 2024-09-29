using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using WebApplication1.ServerApp.DataAccess.Entities;
using WebApplication1.Enums_core_;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApplication1.ServerApp.Сore.Interfaces;
using WebApplication1.ServerApp.Сore.Models;
using WebApplication1.ServerApp.DataAccess;

namespace WebApplication1.ServerApp.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly EventsDbContext _context;

        public UserRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(UserEntity userEntity)
        {
            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();

            return userEntity.Id;
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email);

            return userEntity;
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

        public async Task<List<UserEntity>> GetUsersForEvent(Guid eventId)
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

                return userEntities;
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
                    _context.EventParticipants.Remove(eventParticipate);
                    await _context.SaveChangesAsync();
                }

                return userId;
            }
        }

    }
}
