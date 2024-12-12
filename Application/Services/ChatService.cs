using Application.Interface;
using Data.Repo.Interface;
using Domain.Models;
using Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IRepository<Chat> _repo;
        public ChatService(IRepository<Chat> repo)
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

        public Task<Chat> GetChat(User user, string GUID)
        {
            var chat = user.Chats.FirstOrDefault(x => x.GUID == GUID);
            if (chat == null)
            {
                throw new InvalidOperationException("Chat not found");
            }
            return Task.FromResult(chat);
        }

        public Task<List<Chat>> GetChats(UserDTO user)
        {
            return Task.FromResult(user.Chats);
        }
    }
}

