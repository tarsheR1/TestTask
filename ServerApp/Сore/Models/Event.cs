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

        private Event(Guid id, string title, DateTime date, string location)
        {
            Id = id;
            Title = title;
            EventDateTime = date;
            Location = location;
        }

        public static (Event newEvent, string error) Create(Guid Id, string title, DateTime date, string location)
        {
            var error = string.Empty;

            if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
            {
                error = "Title can't be empty or longer than 100 symbols";
            }

            var _event = new Event(Id, title, date, location);

            return (_event, error);
        }
    }
}
