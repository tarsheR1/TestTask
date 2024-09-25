using WebApplication1.Сore.Contracts;

namespace WebApplication1.Сore.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginUserRequest request);
        Task Register(RegisterUserRequest request);
    }
}