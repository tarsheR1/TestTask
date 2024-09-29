using Microsoft.AspNetCore.Authorization;
using System;
using WebApplication1.Enums_core_;

namespace WebApplication1.ServerApp.Infrastructure.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class PermissionAuthorizeAttribute : Attribute, IAuthorizationPolicyProvider
    {
        public Permissions[] Permissions { get; }

        public PermissionAuthorizeAttribute(params Permissions[] permissions)
        {
            Permissions = permissions;
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new PermissionRequirment(Permissions))
                .Build());
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (policyName == nameof(PermissionAuthorizeAttribute))
            {
                return GetDefaultPolicyAsync();
            }

            return Task.FromResult<AuthorizationPolicy>(null);
        }
    }
}
