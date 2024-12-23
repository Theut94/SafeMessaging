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
    public class MessageService : IMessageService
    {
        private IMessageRepository _repo;

        public MessageService(IMessageRepository repo)
        {
            _repo = repo;
        }
        public async Task<Message> AddMessage(Chat chat, MessageDTO messageDTO)
        {
            var message = new Message() 
            { 
                GUID = Guid.NewGuid().ToString(), 
                IV = messageDTO.IV, 
                Sender = messageDTO.Sender, 
                Text = messageDTO.Text,
                ChatGUID = chat.GUID
            };

            return await _repo.AddAsync(chat, message);            
        }
    }
}
