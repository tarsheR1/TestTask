using System.Linq.Expressions;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Сore.Services
{
    public class EventService : IEventService
    {
        private readonly IEventsRepository _eventsRepository;

        public EventService(IEventsRepository eventsRepository)
        {
            _eventsRepository = eventsRepository;
        }

        public async Task<List<Event>> GetEvents(string? search, string? sortItem, string? order)
        {
            return await _eventsRepository.GetEvents(search, sortItem, order);
        }

        public async Task<Guid> CreateEvent(Event @event)
        {
            return await _eventsRepository.Create(@event);
        }

        public async Task<Guid> UpdateEvent(Guid id, string title, DateTime dateTime, string location)
        {
            return await _eventsRepository.Update(@id, title, dateTime, location);
        }

        public async Task<Guid> DeleteEvent(Guid id)
        {
            return await _eventsRepository.Delete(@id);
        }
    }
}
