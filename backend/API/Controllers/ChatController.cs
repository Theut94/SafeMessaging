using Application.Interface;
using Application.Services;
using Domain.Models;
using Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        private IChatService _chatService;
        private IJWTService _jwtService;
        private IUserService _userService;
        public ChatController(IChatService chatService, IJWTService jwtService, IUserService userService ) 
        {
            _chatService = chatService;
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateChat([FromBody] Chat chat)
        {
            await _chatService.UpdateChat(chat);
            return Ok();
        }

        [HttpGet("GetChat")]
        public async Task<IActionResult> GetChat(string Token, string TargetUser)
        {
            var tokenUserID = _jwtService.DecodeJWTString(Token);
            var user = await _userService.GetUser(tokenUserID);
            var chat =  await _chatService.GetChat(user, TargetUser);
            return Ok(chat);
        }
    }
}
