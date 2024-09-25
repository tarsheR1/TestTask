using System.Linq.Expressions;
using WebApplication1.ServerApp.Сore.Models;

namespace WebApplication1.ServerApp.Сore.Interfaces
{
    public interface IEventsRepository
    {
        Task<List<Event>> GetEvents(string? search, string? sortItem, string? order);
        Task<Guid> Create(Event @event);
        Task<Guid> Update(Guid id, string title, DateTime eventDateTime, string location);
        Task<Guid> Delete(Guid id);
    }
}
