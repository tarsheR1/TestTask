using AutoMapper;
using WebApplication1.ServerApp.DataAccess.Entities;
using WebApplication1.ServerApp.Сore.Contracts;
using WebApplication1.ServerApp.Сore.Interfaces;
using WebApplication1.ServerApp.Сore.Models;

namespace WebApplication1.ServerApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _usersRepository;
        private readonly IJwtProvider _jwtProvider;
        private readonly Mapper _mapper;

        public AuthService(
            IUserRepository usersRepository,
            IPasswordHasher passwordHasher,
            IJwtProvider provider,
            Mapper mapper)
        {
            _passwordHasher = passwordHasher;
            _usersRepository = usersRepository;
            _jwtProvider = provider;
            _mapper = mapper;
        }

        public async Task Register(RegisterUserRequest request)
        {
            var hashedPassword = _passwordHasher.Generate(request.password);

            User user = new User { 
                Id = Guid.NewGuid(), 
                FirstName = request.firstname,
                LastName = request.lastname, 
                DateOfBirth = request.birthdate,
                Email = request.email, 
                PasswordHash = hashedPassword };

            await _usersRepository.Add(_mapper.Map<UserEntity>(user));
        }

        public async Task<string> Login(LoginUserRequest request)
        {
            var user = await _usersRepository.GetByEmail(request.Email);

            var result = _passwordHasher.Verify(request.Password, user.HashPassword);

            if (result == false)
            {
                throw new Exception("failed to login");
            }

            var token = _jwtProvider.GenerateToken(_mapper.Map<User>(user));

            return token;
        }
    }
}
