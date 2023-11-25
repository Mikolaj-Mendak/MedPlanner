using API.Entities;
using API.Enums;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user, UserRoleEnum role);
    }
}
