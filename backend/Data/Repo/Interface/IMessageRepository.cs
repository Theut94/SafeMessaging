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
        public Task<Message> GetByIdAsync(string GUID);

        public Task<IEnumerable<Message>> GetAllAsync();

        public Task AddAsync(Message entity);

        public Task UpdateAsync(Message entity);

        public Task DeleteAsync(Message entity);
    }
}
