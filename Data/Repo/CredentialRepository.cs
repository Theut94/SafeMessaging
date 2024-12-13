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
    public class CredentialRepository : ICredentialRepository
    {
        private readonly IDbContextFactory<Context.Context> _dbContextFactory;

        public CredentialRepository(IDbContextFactory<Context.Context> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        public async Task AddAsync(Credentials entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Credentials.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Credentials entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Credentials.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Credentials>> GetAllAsync()
        {
            await using var context = _dbContextFactory.CreateDbContext();
            return await context.Credentials.ToListAsync();
        }

        public async Task<Credentials?> GetByIdAsync(string GUID)
        {
            await using var context = _dbContextFactory.CreateDbContext();

            return await context.Credentials.FindAsync(GUID);
        }

        public async Task UpdateAsync(Credentials entity)
        {
            await using var context = _dbContextFactory.CreateDbContext();
            context.Credentials.Update(entity);
            await context.SaveChangesAsync();
        }
    }
}
