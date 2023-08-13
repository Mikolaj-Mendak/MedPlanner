using API.Data;
using API.Entities;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
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
        public async Task<ActionResult<User>> AddUser(string password, [FromBody] User user)
        {
            await _userService.AddUserAsync(user, password);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User updatedUser)
        {
            await _userService.UpdateUserAsync(id, updatedUser);
            return NoContent();
        }
    }
}
