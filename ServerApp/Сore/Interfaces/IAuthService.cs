using WebApplication1.ServerApp.Сore.Contracts;

namespace WebApplication1.ServerApp.Сore.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginUserRequest request);
        Task Register(RegisterUserRequest request);
    }
}