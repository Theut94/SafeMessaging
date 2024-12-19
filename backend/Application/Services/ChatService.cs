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

        public async Task UpdateChat(Chat chat)
        {
            await _repo.UpdateAsync(chat);
        }

        public Task<Chat> DeleteChat(User user, Chat chat)
        {
            var chatToDelete = user.Chats.FirstOrDefault(x => x.GUID == chat.GUID);
            if (chatToDelete == null)
            {
                throw new InvalidOperationException("Chat not found");
            }
            user.Chats.Remove(chatToDelete);
            return Task.FromResult(chatToDelete);
        }

        public async Task<ChatDTO> GetChat(User user, string userId)
        {
            var chats = user.Chats;
            foreach(var chat in chats)
            {
                foreach(var targetUser in chat.Users)
                {
                    if(targetUser.GUID == userId)
                    {
                        return new ChatDTO()
                        {
                            GUID = chat.GUID,
                            Messages = chat.Messages
                        };
                    }
                }
            }

            return null;
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

