using Application.Interface;
using Application.Util;
using Data.Repo.Interface;
using Domain.Models;
using Domain.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {

        private IUserRepository _repo;
        private IEncryptionUtil _encryptionUtil;
        
        public UserService(IUserRepository repo, IEncryptionUtil encryptionUtil)
        {
            _repo = repo;
            _encryptionUtil = encryptionUtil;
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

        public async Task<User?> Login(LoginUserDTO loginUserDTO)
        {
            var user = await _repo.GetUserByEmail(loginUserDTO.Username);
            if (user == null)
            {
                var hashedPassword = _encryptionUtil.HashPassword(Encoding.UTF8.GetString(loginUserDTO.Password), loginUserDTO.Salt);
                if(hashedPassword == user.Credentials.Password)
                {
                    return user;
                }                
            }            
            return null;
            
        }

        public async Task Register(RegisterUserDTO registerUserDTO)
        {
           var user = await _repo.GetUserByEmail(registerUserDTO.Username);
           if(user == null)
           {
                var hashedPassword = _encryptionUtil.HashPassword(Encoding.UTF8.GetString(registerUserDTO.Password), registerUserDTO.Salt);
                var userToCreate = new User()
                {
                    FirstName = registerUserDTO.FirstName,
                    LastName = registerUserDTO.LastName,
                    Credentials = new Credentials()
                    {
                        Password = hashedPassword,
                        Salt = registerUserDTO.Salt,
                        UserName = registerUserDTO.Username
                    },
                    GUID = Guid.NewGuid().ToString(),
                    PublickKey = registerUserDTO.PublicKey

                };
                await _repo.AddAsync(userToCreate);
           }
        }
    }
}
