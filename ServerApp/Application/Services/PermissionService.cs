using System.Security;
using WebApplication1.ServerApp.Сore.Interfaces;
using WebApplication1.Enums_core_;

namespace WebApplication1.ServerApp.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IUserRepository _userRepository;

        public PermissionService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<HashSet<Permissions>> GetPermissionsAsync(Guid userId)
        {
            return _userRepository.GetUserPermissions(userId);
        }
    }
}
