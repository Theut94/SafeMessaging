using Data.Repo.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbContextFactory<Context.Context> _dbContextFactory;

        public UserRepository(IDbContextFactory<Context.Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByIdAsync(string GUID)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetUserByEmail(string username)
        {
            username = username.ToLower();

            using var context = await _dbContextFactory.CreateDbContextAsync();
            return context.Set<User>().Where(user => user.Credentials.UserName == username).FirstOrDefault();
        }

        public Task UpdateAsync(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
