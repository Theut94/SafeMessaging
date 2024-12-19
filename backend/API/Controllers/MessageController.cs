using Application.Interface;
using Domain.Models;
using Domain.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private IMessageService _messageService;
        private IChatService _chatService;

        public MessageController(IMessageService messageService, IChatService chatservice)
        {
            _messageService = messageService;
            _chatService = chatservice;
        }

        [HttpPost("PostMessage")]
        public async Task<IActionResult> PostMessage(MessageDTO message, string chatid)
        {
            Chat chat = await _chatService.GetChatById(chatid);
            if (chat == null)
            {
                return NotFound("Chat not found with chatid: " + chatid );
            }
            await _messageService.AddMessage(chat, message);
            return Ok();
        }



    }
}
