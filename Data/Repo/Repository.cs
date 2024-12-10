using Data.Repo.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo
{
        public class Repository<T> : IRepository<T> where T : class
        {
            private readonly IDbContextFactory<Context.Context> _dbContextFactory;

            public Repository(IDbContextFactory<Context.Context> dbContextFactory)
            {
                _dbContextFactory = dbContextFactory;
            }

            public async Task<T?> GetByIdAsync(string Guid)
            {
                await using var context = _dbContextFactory.CreateDbContext();
                return await context.Set<T>().FindAsync(Guid);
            }

            public async Task<IEnumerable<T>> GetAllAsync()
            {
                await using var context = _dbContextFactory.CreateDbContext();
                return await context.Set<T>().ToListAsync();
            }

            public async Task AddAsync(T entity)
            {
                await using var context = _dbContextFactory.CreateDbContext();
                context.Set<T>().Add(entity);
                await context.SaveChangesAsync();
            }

            public async Task UpdateAsync(T entity)
            {
                await using var context = _dbContextFactory.CreateDbContext();
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();
            }

            public async Task DeleteAsync(T entity)
            {
                await using var context = _dbContextFactory.CreateDbContext();
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

    }

