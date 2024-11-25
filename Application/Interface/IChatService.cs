using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IChatService
    {
        public Task<List<Chat>> GetChats();
        public Task<Chat> GetChat(string GUID);
    }
}
