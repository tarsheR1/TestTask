using WebApplication1.Contracts;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Сore.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(IUserRepository usersRepository, IPasswordHasher passwordHasher, IJwtProvider provider)
        {
            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _jwtProvider = provider;
        }

        public async Task Register(RegisterUserRequest request)
        {
            var hashedPassword = _passwordHasher.Generate(request.password);

            var user = User.Create(Guid.NewGuid(), request.firstname, request.lastname, request.birthdate, request.email, hashedPassword);

            await _usersRepository.Add(user);
        }

        public async Task<string> Login(LoginUserRequest request)
        {
            var user = await _usersRepository.GetByEmail(request.Email);

            var result = _passwordHasher.Verify(request.Password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return token;
        }
    }
}
