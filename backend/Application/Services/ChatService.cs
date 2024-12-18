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

        public Task<Chat> CreateChat(User user, Chat chat)
        {
            if (user.Chats.Any(x => x.GUID == chat.GUID))
            {
                throw new InvalidOperationException("Chat already exists");
            }
            user.Chats.Add(chat);
            return Task.FromResult(chat);
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

        public async Task<Chat> GetChatById(string chatid)
        {
           return await _repo.GetByIdAsync(chatid);
        }
    }
}

