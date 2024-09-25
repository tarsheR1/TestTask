namespace WebApplication1.ServerApp.DataAccess.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string HashPassword { get; set; }
        public string Email { get; set; }

        public List<EventEntity> EventPart { get; set; } = [];

        public ICollection<RoleEntity> Roles { get; set; } = [];
    }
}
