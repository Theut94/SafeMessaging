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
    }
}
