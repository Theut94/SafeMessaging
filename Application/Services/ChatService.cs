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
    public class ChatService : IChatService
    {
        private readonly IRepository<Chat> _repo;
        public ChatService(IRepository<Chat> repo)
        {
            _repo = repo;
        }
        public Task<Chat> GetChat(string GUID)
        {
            throw new NotImplementedException();
        }

        public Task<List<Chat>> GetChats()
        {
            throw new NotImplementedException();
        }
    }
}

