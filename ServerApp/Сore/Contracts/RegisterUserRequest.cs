using System.ComponentModel.DataAnnotations;

namespace WebApplication1.ServerApp.Сore.Contracts
{
    public record RegisterUserRequest(
        [Required] string firstname,
        [Required] string lastname,
        [Required] DateTime birthdate,
        [Required] string email,
        [Required] string password
    );
}

