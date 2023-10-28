using API.Dtos;
using API.Entities;

namespace API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserByIdAsync(Guid id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(Guid id, User updatedUser);
        Task<User> GetUserByEmailAsync(string email);
        Task<UserDetailsDto> GetUserDetailsByEmailAsync(string email);
    }
}
