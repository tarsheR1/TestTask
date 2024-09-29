using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Data;
using System.Net.WebSockets;
using System.Security;
using WebApplication1.ServerApp.DataAccess.Entities;
using WebApplication1.Enums_core_;
using WebApplication1.ServerApp.Infrastructure.Authorization;

namespace WebApplication1.ServerApp.DataAccess.Configuration
{
    public class RolePermissionConfiguration
        : IEntityTypeConfiguration<RolePermissionEntity>
    {
        private readonly AuthorizationOptions _authorization;

        public RolePermissionConfiguration(AuthorizationOptions authorization)
        {
            _authorization = authorization;
        }

        public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
        {
            //builder.HasKey(r => new { r.RoleId, r.PermissionId });
            builder.HasData(ParseRolePermissions());
        }

        private List<RolePermissionEntity> ParseRolePermissions()
        {

            try
            {
                var rolePermissions = _authorization.RolePermissions
                    .SelectMany(rp => rp.Permissions
                        .Select(p => new RolePermissionEntity
                        {
                            RoleId = Enum.TryParse<Roles>(rp.Role, out var role) ? (int)role : throw new ArgumentException($"Invalid role: {rp.Role}"),
                            PermissionId = Enum.TryParse<Permissions>(p, out var permission) ? (int)permission : throw new ArgumentException($"Invalid permission: {p}")
                        }))
                .ToList();
                Console.WriteLine($"Добавление роли: {rolePermissions}=");

                return rolePermissions;
            }
            catch (ArgumentException ex)
            {
                // Обработка специфичных ошибок аргументов
                Console.WriteLine($"Argument error: {ex.Message}");
                throw; // Можно выбросить дальше или обработать по-другому
            }
            catch (Exception ex)
            {
                // Обработка всех остальных ошибок
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Можно выбросить дальше или обработать по-другому
            }
        }
    }
}
