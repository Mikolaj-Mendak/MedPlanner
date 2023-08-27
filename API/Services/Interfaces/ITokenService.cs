using API.Entities;

namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
