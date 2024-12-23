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
        public async Task<Chat> AddAsync(Chat entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();

            // Attach users if they are already tracked (to avoid tracking duplicates)
            var distinctUsers = entity.Users
                .GroupBy(u => u.GUID)
                .Select(g => g.First())
                .ToList();

            entity.Users = distinctUsers;

            foreach (var user in entity.Users)
            {
                if (context.Entry(user).State == EntityState.Detached)
                {
                    context.Users.Attach(user); // Attach the user to the context without re-adding it
                }
            }

            context.Chats.Add(entity);
            await context.SaveChangesAsync();
            return entity;
        }

 
        public async Task<Chat?> GetByIdAsync(string GUID)
        {
            await using var context = _dbContextFactory.CreateDbContext();

            return await context.Chats.Where(chat => chat.GUID == GUID).Include(chat => chat.Messages).FirstOrDefaultAsync();
        }

        public async Task<Chat?> GetByUserIDsAsync(string userId, string targetUserId){
            await using var context = _dbContextFactory.CreateDbContext();

            var chat = await context.Chats
                .Include(c => c.Users)
                .Include(c => c.Messages)
                .FirstOrDefaultAsync(c => userId == targetUserId ? //If the IDs are the same a user is requesting a chat with themselves
                    c.Users.Count == 1 && c.Users.Any(u => u.GUID == userId)
                    : //Otherwise we check that both IDs exist in the user list of the chat
                    c.Users.Any(u => u.GUID == userId) && 
                    c.Users.Any(u => u.GUID == targetUserId));
                                

            return chat;
        }

    }
}
