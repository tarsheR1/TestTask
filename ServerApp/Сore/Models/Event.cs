using WebApplication1.ServerApp.DataAccess.Entities;

namespace WebApplication1.ServerApp.Сore.Models
{
    public class Event
    {
        public const int MAX_TITLE_LENGTH = 100;

        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; } = string.Empty;
        public DateTime EventDateTime { get; init; }
        public string Location { get; init; }
        public string Category { get; init; } = string.Empty;
        public int MaxParticipants { get; init; } = 0;
        public string ImageUrl { get; init; } = string.Empty;
        public List<User> UserSubscribed { get; set; } = [];

        public Event()
        {
        }
    }
}