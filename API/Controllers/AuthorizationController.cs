﻿using API.Dtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthorizationController : BaseController
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

    }
}
