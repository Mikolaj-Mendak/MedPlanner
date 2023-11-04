using API.Enums;

namespace API.Dtos
{
    public class UserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string Id { get; set; }
        public UserRoleEnum Role { get; set; }
    }
}
