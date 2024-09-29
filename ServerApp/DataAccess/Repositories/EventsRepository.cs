using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Web.WebPages;
using System.Linq.Expressions;
using System.Linq;
using WebApplication1.ServerApp.DataAccess.Entities;
using WebApplication1.ServerApp.Сore.Interfaces;
using WebApplication1.ServerApp.Сore.Models;
using WebApplication1.ServerApp.DataAccess;

namespace WebApplication1.ServerApp.DataAccess.Repositories
{
    public class EventsRepository : IEventsRepository
    {
        private readonly EventsDbContext _context;

        public EventsRepository(EventsDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetEvents(string? search, string? sortItem, string? sortOrder)
        {
            var eventsEntities = await _context.Events
                .Where(e => string.IsNullOrWhiteSpace(search) ||
                            e.Title.ToLower().Contains(search.ToLower()))
                .AsNoTracking()
                .ToListAsync();

            var events = eventsEntities
                    .Select(e => new Event { Id = e.Id, Title = e.Title, EventDateTime = e.EventDateTime, Location = e.Location })
                    .ToList();

            return events;
        }

        public async Task<Guid> Create(EventEntity eventEntity)
        {
            
            await _context.Events.AddAsync(eventEntity);
            await _context.SaveChangesAsync();

            return eventEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, DateTime eventDateTime, string location)
        {
            await _context.Events
                .Where(e => e.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(e => e.Title, e => title)
                    .SetProperty(e => e.EventDateTime, e => eventDateTime)
                    .SetProperty(e => e.Location, e => location));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Events
                .Where(e => e.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
