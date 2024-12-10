using Application.Interface;
using Data.Repo.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class MessageService : IMessageService
    {
        private IRepository<Message> _repo;

        public MessageService(IRepository<Message> repo)
        {
            _repo = repo;
        }

        public Task<List<Message>> GetMessages(Chat chat)
        {
            return Task.FromResult(chat.Messages ?? new List<Message>());
        }

        public Task<Message> SendMessage(Chat chat, Message message)
        {
            if (chat.Messages == null)
            {
                chat.Messages = new List<Message>();
            }
            chat.Messages.Add(message);
            return Task.FromResult(message);
        }

        public Task<Message> DeleteMessage(Chat chat, Message message)
        {
            var messageToDelete = chat.Messages?.FirstOrDefault(x => x.GUID == message.GUID);
            if (messageToDelete == null)
            {
                throw new InvalidOperationException("Message not found");
            }
            if (chat.Messages != null)
            {
                chat.Messages.Remove(messageToDelete);
            }
            return Task.FromResult(messageToDelete);
        }

        public Task<Message> EditMessage(Chat chat, Message message)
        {
            var messageToEdit = chat.Messages?.FirstOrDefault(x => x.GUID == message.GUID);
            if (messageToEdit == null)
            {
                throw new InvalidOperationException("Message not found");
            }
            if (chat.Messages != null)
            {
                chat.Messages.Remove(messageToEdit);
                chat.Messages.Add(message);
            }
            return Task.FromResult(message);
        }
    }
}
