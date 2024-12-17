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
        public Task<User> EditUser(User user);
        public Task<User> DeleteUser(User user);
        public Task<User> AddFriend(User user, User friend);
        public Task<User> RemoveFriend(User user, User friend);
        public Task<List<User>> GetAllUsers();

        public Task<User> Login(LoginUserDTO registerUserDTO);
        public Task Register(RegisterUserDTO registerUserDTO);
    }
}
