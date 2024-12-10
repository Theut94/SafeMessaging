using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUserByEmail(string username);
    }
}
