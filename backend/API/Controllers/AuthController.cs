﻿using Application.Interface;
using Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private IJWTService _jwtService;
        private IUserService _userService;
        
        public AuthController(IJWTService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO loginUserDTO)
        {
            var user =  await _userService.Login(loginUserDTO);
            if (user == null)
            {
                return BadRequest();
            }
            var token = _jwtService.CreateTokenWithAttributes(user);
            //CHECK CREDENTIALS THROUGH SERVICE.
            return Ok(new { token , expiration = DateTime.UtcNow.AddMinutes(30)} );
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            registerUserDTO.Username = registerUserDTO.Username.ToLower();
            await _userService.Register(registerUserDTO);
            return Ok();
        }
    }
}
