using API.Entities;

namespace API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUserByIdAsync(Guid id);
        Task AddUserAsync(User user, string password);
        Task UpdateUserAsync(Guid id, User updatedUser);
        Task<bool> VerifyPasswordAsync(User user, string password);

    }
}
