using API.Dtos;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<bool> VerifyPasswordAsync(User user, string password);
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> RegisterAsync(RegisterDto registerDto);
        string GenerateJwtToken(User user);
        Task<DoctorDto> RegisterDoctorAsync(DoctorRegisterDto registerDoctorDto);
        Task<OwnerDto> RegisterOwnerAsync(RegisterOwnerDto registerOwnerDto);
    }
}
