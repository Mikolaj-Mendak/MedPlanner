using API.Dtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public AuthorizationController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var authResult = await _authorizationService.RegisterAsync(registerDto);

            if (authResult == null)
            {
                return Conflict("User with this email already exists.");
            }

            return Ok(authResult);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginAsync([FromBody] LoginDto loginDto)
        {
            var userDto = await _authorizationService.LoginAsync(loginDto);

            if (userDto == null)
            {
                return Unauthorized();
            }

            return userDto;
        }

        [HttpPost("doctorRegister")]
        public async Task<ActionResult> DoctorRegister([FromBody] DoctorRegisterDto registerDto)
        {
            var authResult = await _authorizationService.RegisterDoctorAsync(registerDto);

            if (authResult == null)
            {
                return Conflict("User with this email already exists.");
            }

            return Ok(authResult);
        }

        [HttpPost("ownerRegister")]
        public async Task<ActionResult> OwnerRegister([FromBody] RegisterOwnerDto registerDto)
        {
            var authResult = await _authorizationService.RegisterOwnerAsync(registerDto);

            if (authResult == null)
            {
                return Conflict("User with this email already exists.");
            }

            return Ok(authResult);
        }

    }
}
