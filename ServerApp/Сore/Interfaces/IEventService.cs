using System.Linq.Expressions;
using WebApplication1.ServerApp.Сore.Models;

namespace WebApplication1.ServerApp.Сore.Interfaces
{
    public interface IEventService
    {
        Task<List<Event>> GetEvents(string? search, string? sortItem, string? order);
        Task<Guid> CreateEvent(Event @event);
        Task<Guid> UpdateEvent(Guid id, string title, DateTime dateTime, string location);
        Task<Guid> DeleteEvent(Guid id);
    }
}
