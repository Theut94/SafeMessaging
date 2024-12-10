using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interface
{
    public interface IMessageService
    {
        public Task<List<Message>> GetMessages(Chat chat);
        public Task<Message> SendMessage(Chat chat, Message message);
        public Task<Message> EditMessage(Chat chat, Message message);
        public Task<Message> DeleteMessage(Chat chat, Message message);
    }
}
