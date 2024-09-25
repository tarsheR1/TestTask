using Microsoft.AspNetCore.Authorization;
using WebApplication1.Enums_core_;
namespace WebApplication1.Infrastructure.Authorization
{
    public class PermissionRequirment(Permissions[] permissions) : IAuthorizationRequirement
    {
        public Permissions[] Permissions { get; set; } = permissions;
    }
}
