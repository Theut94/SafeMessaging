using Application.Interface;
using Application.Util;
using Data.Repo.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {

        private IRepository<User> _repo;
        private IEncryptionUtil _encryptionUtil;
        
        public UserService(IRepository<User> repo, IEncryptionUtil encryptionUtil)
        {
            _repo = repo;
            _encryptionUtil = encryptionUtil;
        }

        public Task<User> CreateUser(User user)
        {
            if (_repo.GetAllAsync().Result.Any(x => x.GUID == user.GUID))
            {
                throw new InvalidOperationException("User already exists");
            }
            _repo.AddAsync(user);
            return Task.FromResult(user);
        }

        public Task<User> GetUser(string GUID)
        {
            var user = _repo.GetAllAsync().Result.FirstOrDefault(x => x.GUID == GUID);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }
            return Task.FromResult(user);
        }

        public Task<User> AddFriend(User user, User friend)
        {

            if (user.FriendList.Any(x => x.GUID == friend.GUID))
            {
                throw new InvalidOperationException("Friend already exists");
            }
            user.FriendList.Add(friend);
            return Task.FromResult(user);
        }

        public Task<User> DeleteUser(User user)
        {
            var userToDelete = _repo.GetAllAsync().Result.FirstOrDefault(x => x.GUID == user.GUID);
            if (userToDelete == null)
            {
                throw new InvalidOperationException("User not found");
            }
            _repo.DeleteAsync(userToDelete);
            return Task.FromResult(userToDelete);
        }

        public Task<User> EditUser(User user)
        {
            var userToEdit = _repo.GetAllAsync().Result.FirstOrDefault(x => x.GUID == user.GUID);
            if (userToEdit == null)
            {
                throw new InvalidOperationException("User not found");
            }
            _repo.UpdateAsync(user);
            return Task.FromResult(user);
        }

        public Task<User> RemoveFriend(User user, User friend)
        {
            var friendToRemove = user.FriendList.FirstOrDefault(x => x.GUID == friend.GUID);
            if (friendToRemove == null)
            {
                throw new InvalidOperationException("Friend not found");
            }
            user.FriendList.Remove(friendToRemove);
            return Task.FromResult(user);
        }

        public Task<User> Login(string username, byte[] password)
        {
            throw new NotImplementedException();
        }

        public Task<User> Register(string username, byte[] password)
        {
         

            _repo.
        }
    }
}
