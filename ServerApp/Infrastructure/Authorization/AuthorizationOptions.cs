﻿namespace WebApplication1.ServerApp.Infrastructure.Authorization
{
    public class AuthorizationOptions
    {
        public RolePermissions[] RolePermissions { get; set; } = [];
    }

    public class RolePermissions
    {
        public string Role { get; set; } = string.Empty;

        public string[] Permissions { get; set; } = [];
    }
}
