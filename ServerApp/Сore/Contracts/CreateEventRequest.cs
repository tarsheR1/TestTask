using System.Runtime.Serialization;

namespace WebApplication1.ServerApp.Сore.Contracts
{
    public record CreateEventRequest(string Title, DateTime DateTime, string Location);
}
