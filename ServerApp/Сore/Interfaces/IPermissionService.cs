﻿using WebApplication1.Enums_core_;

namespace WebApplication1.ServerApp.Сore.Interfaces
{
    public interface IPermissionService
    {
        Task<HashSet<Permissions>> GetPermissionsAsync(Guid userId);
    }
}