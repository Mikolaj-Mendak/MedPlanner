using API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Services
{
    public class UserProviderService : IUserProviderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserProviderService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            var user = _httpContextAccessor.HttpContext.User;
            if (user != null)
            {
                var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier); 
                if (userIdClaim != null)
                {
                    return userIdClaim.Value;
                }
            }

            return null;
        }

    }
}
