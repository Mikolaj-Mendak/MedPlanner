using API.Dtos;
using API.Entities;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUsersService _userService;

        public UsersController(IUsersService userService)
        {
            _userService = userService;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var result = await _userService.GetUsers(); 
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task <ActionResult<User>> GetUserById(Guid id)
        {
            var user = _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(await user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            await _userService.AddUserAsync(user);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User updatedUser)
        {
            await _userService.UpdateUserAsync(id, updatedUser);
            return NoContent();
        }

        [HttpGet("details/by-email")]
        public async Task<ActionResult<UserDetailsDto>> GetUserDetailsByEmailAsync([FromQuery] string email)
        {
            var user = await _userService.GetUserDetailsByEmailAsync(email);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


    }
}
