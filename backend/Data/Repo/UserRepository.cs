using Data.Repo.Interface;
using Domain.Models;
using Domain.Models.DTO;
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
        public async Task<User?> GetByIdAsync(string Guid)
        {
            await using var context = _dbContextFactory.CreateDbContext();

            return await context.Users.FindAsync(Guid);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Users.ToListAsync();
        }

        public async Task AddAsync(User entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Users.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Users.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Users.Remove(entity);
            await context.SaveChangesAsync();
        }
        public async Task<User?> GetUserByEmail(string username)
        {
            username = username.ToLower();

            using var context = await _dbContextFactory.CreateDbContextAsync();
            return context.Users.Where(user => user.Credentials.UserName == username).Include(u => u.Credentials).Include(u => u.Chats).FirstOrDefault();
        }


    }
}
