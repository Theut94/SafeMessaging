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
    public class MessageRepository : IMessageRepository
    {
        private readonly IDbContextFactory<Context.Context> _dbContextFactory;

        public MessageRepository(IDbContextFactory<Context.Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task AddAsync(Message entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Messages.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Message entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Messages.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Messages.ToListAsync();
        }

        public async Task<Message?> GetByIdAsync(string GUID)
        {
            await using var context = _dbContextFactory.CreateDbContext();

            return await context.Messages.FindAsync(GUID);
        }

        public async Task UpdateAsync(Message entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Messages.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
