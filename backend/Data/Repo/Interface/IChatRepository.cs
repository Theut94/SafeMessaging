using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo.Interface
{
    public interface IChatRepository
    {
        public Task<Chat> GetByIdAsync(string userIds);
        public Task<Chat> GetByUserIDsAsync(string userId, string targetUserId);
        public Task<Chat> AddAsync(Chat entity);

    }
}
