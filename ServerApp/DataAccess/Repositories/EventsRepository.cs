using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System.Web.WebPages;
using System.Linq.Expressions;
using System.Linq;
using WebApplication1.DataAccess.Entities;
using WebApplication1.Сore.Interfaces;
using WebApplication1.Сore.Models;

namespace WebApplication1.DataAccess.Repositories
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
                .Select(e => Event.Create(e.Id, e.Title, e.EventDateTime, e.Location).newEvent)
                .ToList();

            return events;
        }

        public async Task<Guid> Create(Event @event)
        {
            var eventEntity = new EventEntity
            {
                Id = @event.Id,
                Title = @event.Title,
                EventDateTime = @event.EventDateTime,
                Location = @event.Location
            };

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
