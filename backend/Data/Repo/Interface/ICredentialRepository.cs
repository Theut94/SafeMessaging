using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo.Interface
{
    public interface ICredentialRepository 
    {
        public Task<Credentials> GetByIdAsync(string UserName);

        public Task<IEnumerable<Credentials>> GetAllAsync();

        public Task AddAsync(Credentials entity);

        public Task UpdateAsync(Credentials entity);

        public Task DeleteAsync(Credentials entity);
    }
}
