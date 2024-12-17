using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo.Interface
{
    public interface IUserRepository
    {
        public Task<User?> GetByIdAsync(string GUID);

        public Task<IEnumerable<User>> GetAllAsync();

        public Task AddAsync(User entity);

        public Task UpdateAsync(User entity);

        public Task DeleteAsync(User entity);
        Task<User> GetUserByEmail(string username);
    }
}
