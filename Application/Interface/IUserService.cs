using Domain.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IUserService
    {
        public Task<User> GetUser();

        public Task<User> Login(string username, byte[] password);
        public Task<User> Register(string username, byte[] password);
    }
}
