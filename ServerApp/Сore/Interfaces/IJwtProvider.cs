using WebApplication1.ServerApp.Сore.Models;

namespace WebApplication1.ServerApp.Сore.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}