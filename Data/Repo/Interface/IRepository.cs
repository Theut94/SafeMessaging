using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo.Interface
{
    public interface IRepository<T> where T : class
    {
        public Task<T?> GetByIdAsync(string GUID);

        public Task<IEnumerable<T>> GetAllAsync();

        public Task AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(T entity);
    }
}
