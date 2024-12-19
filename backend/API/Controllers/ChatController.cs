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

        [HttpGet("GetChat")]
        public async Task<IActionResult> GetChat([FromHeader(Name = "Authorization")]string Authorization, [FromQuery]string TargetUser)
        {
            var token = Authorization;
            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required.");
            }

            var tokenUserID = _jwtService.DecodeJWTString(token);
            var user = await _userService.GetUser(tokenUserID);
            var targetUser = await _userService.GetUser(TargetUser);
            var chat =  await _chatService.GetChatByUserIDs(user, targetUser);

            if(chat == null) {
                
                chat = await _chatService.CreateChat(user, targetUser);
            }

            return Ok(chat);
        }
    }
}
