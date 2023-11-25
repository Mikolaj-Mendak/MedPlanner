using API.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Authorization
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class RolesAuthorization : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly UserRoleEnum[] _allowedRoles;

        public RolesAuthorization(params UserRoleEnum[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            var hasRequiredRole = _allowedRoles.Any(requiredRole =>
                user.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == requiredRole.ToString()));

            if (!hasRequiredRole)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
