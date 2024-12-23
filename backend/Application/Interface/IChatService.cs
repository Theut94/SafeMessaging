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
        public Task<ChatDTO> GetChatByUserIDs(User user, User targetUser);

        public Task<ChatDTO> CreateChat(User user,  User targetUser);

        public Task<Chat> GetChatById(string chatid);
    }
}
