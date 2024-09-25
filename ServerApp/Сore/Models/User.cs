namespace WebApplication1.Сore.Models
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

        public User(Guid id, string firstname, string lastname, DateTime birthdate, string email, string passwordHash)
        {
            Id = id;
            FirstName = firstname;
            LastName = lastname;
            DateOfBirth = birthdate;
            RegistrationDate = DateTime.UtcNow;
            Email = email;
            PasswordHash = passwordHash;
        }

        public static User Create(Guid id, string firstname, string lastname, DateTime birthdate, string email, string password)
        {
            var error = string.Empty;

            User newUser = new User(id, firstname, lastname, birthdate, email, password);

            return newUser;
        }
    }
}
