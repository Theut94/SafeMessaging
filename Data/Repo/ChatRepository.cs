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
    public class ChatRepository : IChatRepository
    {
        private readonly IDbContextFactory<Context.Context> _dbContextFactory;

        public ChatRepository(IDbContextFactory<Context.Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task AddAsync(Chat entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Chats.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Chat entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Chats.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Chat>> GetAllAsync()
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Chats.ToListAsync();
        }

        public async Task<Chat?> GetByIdAsync(string GUID)
        {
            await using var context = _dbContextFactory.CreateDbContext();

            return await context.Chats.FindAsync(GUID);
        }

        public async Task UpdateAsync(Chat entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Chats.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
