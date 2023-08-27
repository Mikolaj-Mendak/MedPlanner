using API.Authorization.Helpers;
using API.Dtos;
using API.Entities;
using API.Services.Interfaces;

namespace API.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;

        public AuthorizationService(IUsersService usersService, ITokenService tokenService)
        {
            _usersService = usersService;
            _tokenService = tokenService;
        }

        public async Task<bool> VerifyPasswordAsync(User user, string password)
        {
            return PasswordHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        }

        public string GenerateJwtToken(User user)
        {
            return _tokenService.GenerateToken(user);
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _usersService.GetUserByEmailAsync(loginDto.Email);

            if (user == null || !await VerifyPasswordAsync(user, loginDto.Password))
            {
                return null;
            }

            var token = GenerateJwtToken(user);

            var userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };

            return userDto;
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await _usersService.GetUserByEmailAsync(registerDto.Email) != null)
            {
                return null;
            }

            PasswordHelper.CreatePasswordHash(registerDto.Password, out var passwordHash, out var passwordSalt);

            var newUser = new User
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _usersService.AddUserAsync(newUser);

            var token = GenerateJwtToken(newUser);

            var authResult = new UserDto
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Token = token
            };

            return authResult;
        }


    }
}
