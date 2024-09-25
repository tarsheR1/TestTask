namespace WebApplication1.ServerApp.DataAccess.Entities
{
    public class EventEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime EventDateTime { get; set; }
        public string Location { get; set; }
        public string Category { get; set; } = string.Empty;
        public int MaxParticipants { get; set; } = 0;
        public string ImageUrl { get; set; } = string.Empty;

        public List<UserEntity> UserSubscribed { get; set; } = [];
    }
}
