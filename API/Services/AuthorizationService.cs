using API.Authorization.Helpers;
using API.Dtos;
using API.Entities;
using API.Enums;
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
                Token = token,
                Role = user.Role
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
                PasswordSalt = passwordSalt,
                Pesel = registerDto.Pesel,
                Role = UserRoleEnum.User
            };

            await _usersService.AddUserAsync(newUser);

            var token = GenerateJwtToken(newUser);

            var authResult = new UserDto
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Token = token,
                Role = newUser.Role
            };

            return authResult;
        }

        public async Task<DoctorDto> RegisterDoctorAsync(DoctorRegisterDto registerDoctorDto)
        {
            if (await _usersService.GetUserByEmailAsync(registerDoctorDto.Email) != null)
            {
                return null; 
            }

            PasswordHelper.CreatePasswordHash(registerDoctorDto.Password, out var passwordHash, out var passwordSalt);

            var newDoctor = new Doctor
            {
                FirstName = registerDoctorDto.FirstName,
                LastName = registerDoctorDto.LastName,
                Email = registerDoctorDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                DoctorNumber = registerDoctorDto.DoctorNumber,
                Pesel = registerDoctorDto.Pesel,
                Role = UserRoleEnum.Doctor

            };

            await _usersService.AddUserAsync(newDoctor);

            var token = GenerateJwtToken(newDoctor);

            var authResult = new DoctorDto
            {
                FirstName = newDoctor.FirstName,
                LastName = newDoctor.LastName,
                Email = newDoctor.Email,
                Token = token,
                DoctorNumber = newDoctor.DoctorNumber,
                Role = newDoctor.Role
            };

            return authResult;
        }

        public async Task<OwnerDto> RegisterOwnerAsync(RegisterOwnerDto registerOwnerDto)
        {
            if (await _usersService.GetUserByEmailAsync(registerOwnerDto.Email) != null)
            {
                return null;
            }

            PasswordHelper.CreatePasswordHash(registerOwnerDto.Password, out var passwordHash, out var passwordSalt);

            var newOwner = new ClinicOwner
            {
                FirstName = registerOwnerDto.FirstName,
                LastName = registerOwnerDto.LastName,
                Email = registerOwnerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Pesel = registerOwnerDto.Pesel,
                Clinic = registerOwnerDto?.Clinic,
                Role = UserRoleEnum.ClinicOwner,

            };

            await _usersService.AddUserAsync(newOwner);

            var token = GenerateJwtToken(newOwner);

            var authResult = new OwnerDto
            {
                FirstName = newOwner.FirstName,
                LastName = newOwner.LastName,
                Email = newOwner.Email,
                Token = token,
                Clinic = newOwner.Clinic,
                Role = newOwner.Role
            };

            return authResult;
        }

    }
}
