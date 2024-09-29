namespace WebApplication1.ServerApp.Сore.Models
{
    public class User
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public DateTime DateOfBirth { get; init; }
        public DateTime RegistrationDate { get; init; }
        public string Email { get; init; }
        public string PasswordHash { get; init; }


        public User()
        {
        }
    }
}
