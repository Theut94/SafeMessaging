using Domain.Models;
using Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IChatService
    {
        public Task<ChatDTO> GetChat(User user, string  userId);
        public Task<Chat> CreateChat(User user, Chat chat);
        public Task<Chat> DeleteChat(User user, Chat chat);
        public Task UpdateChat(Chat chat);
        public Task<Chat> GetChatById(string chatid);
    }
}
