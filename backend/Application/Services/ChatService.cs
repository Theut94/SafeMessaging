using Application.Interface;
using Data.Repo.Interface;
using Domain.Models;
using Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _repo;
        public ChatService(IChatRepository repo)
        {
            _repo = repo;
        }

        public async Task<ChatDTO> CreateChat(User user, User targetUser)
        {
            
            Chat chat = new Chat
            {     
                GUID = Guid.NewGuid().ToString(),
                Users = new List<User> { user, targetUser },
                Messages = new List<Message>()
            };
            chat = await _repo.AddAsync(chat);

            return new ChatDTO {
                GUID = chat.GUID,
                Messages = chat.Messages
            };
        }


        public async Task<ChatDTO> GetChatByUserIDs(User user, User targetUser){
            var chat = await _repo.GetByUserIDsAsync(user.GUID, targetUser.GUID);

            if(chat == null){
                return null;
            }

            ChatDTO chatDTO = new ChatDTO {
                GUID = chat.GUID,
                Messages = chat.Messages
            };

            return chatDTO;
        }

        public async Task<Chat> GetChatById(string chatid)
        {
           return await _repo.GetByIdAsync(chatid);
        }
    }
}

