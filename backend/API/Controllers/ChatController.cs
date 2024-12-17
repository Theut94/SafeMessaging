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
        public ChatController(IChatService chatService) 
        {
            _chatService = chatService;
        }
        [HttpGet]
        public async Task<List<Chat>> GetChats([FromBody] UserDTO userDTO)
        {
            return await _chatService.GetChats(userDTO);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateChat([FromBody] Chat chat)
        {
            await _chatService.UpdateChat(chat);
            return Ok();
        }
    }
}
