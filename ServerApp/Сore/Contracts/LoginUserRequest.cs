using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ServerApp.Сore.Contracts
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
