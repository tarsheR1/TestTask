using Microsoft.AspNetCore.Authorization;
using System;
using WebApplication1.Authorization;
using WebApplication1.Enums_core_;

namespace WebApplication1.Infrastructure.Authorization
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
            // Если имя политики совпадает с именем, которое мы ожидаем, возвращаем нужную политику
            if (policyName == nameof(PermissionAuthorizeAttribute))
            {
                return GetDefaultPolicyAsync();
            }

            // Возвращаем null, если политика не найдена
            return Task.FromResult<AuthorizationPolicy>(null);
        }
    }
}
