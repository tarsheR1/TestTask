using AutoMapper;
using System.Linq.Expressions;
using WebApplication1.ServerApp.DataAccess.Entities;
using WebApplication1.ServerApp.Сore.Interfaces;
using WebApplication1.ServerApp.Сore.Models;

namespace WebApplication1.ServerApp.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventsRepository _eventsRepository;
        private readonly Mapper _mapper;

        public EventService(IEventsRepository eventsRepository, Mapper mapper)
        {
            _eventsRepository = eventsRepository;
            _mapper = mapper;
        }

        public async Task<List<Event>> GetEvents(string? search, string? sortItem, string? order)
        {
            return await _eventsRepository.GetEvents(search, sortItem, order);
        }

        public async Task<Guid> CreateEvent(Event @event)
        {
            return await _eventsRepository.Create(_mapper.Map<EventEntity>(@event));
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
