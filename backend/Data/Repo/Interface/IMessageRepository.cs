using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo.Interface
{
    public interface IMessageRepository
    {
        public Task<Message> AddAsync(Chat chat, Message message);
    }
}
