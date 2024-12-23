using Domain.Models;
using Domain.Models.DTO;
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
        public Task<User> GetUser(string GUID);
        public Task<List<User>> GetAllUsers();
        public Task<User> Login(LoginUserDTO registerUserDTO);
        public Task Register(RegisterUserDTO registerUserDTO);
    }
}
