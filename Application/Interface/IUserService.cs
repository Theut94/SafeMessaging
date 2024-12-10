﻿using Domain.Models;
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

        public Task<User> Login(string username, byte[] password);
        public Task Register(string username, byte[] password, string firstName, string lastName, string PublicKey);
    }
}
