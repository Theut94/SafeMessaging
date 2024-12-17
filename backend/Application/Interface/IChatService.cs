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
        public Task<List<Chat>> GetChats(UserDTO user);
        public Task<Chat> GetChat(User user, string GUID);
        public Task<Chat> CreateChat(User user, Chat chat);
        public Task<Chat> DeleteChat(User user, Chat chat);
        public Task UpdateChat(Chat chat);
    }
}
