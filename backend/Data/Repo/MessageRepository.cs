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
        public async Task<Message> AddAsync(Chat chat, Message message)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Messages.Add(message);
            await context.SaveChangesAsync();
            return message;
        }       
    }
}
