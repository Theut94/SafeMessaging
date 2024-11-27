using Application.Interface;
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
        
        public UserService(IRepository<User> repo)
        {
            _repo = repo;
        }

        public Task<User> GetUser()
        {
            return Task.FromResult(new User()
            {
                GUID = Guid.NewGuid().ToString(),
                Chats = new List<Chat>() {  new Chat
                {
                    GUID = Guid.NewGuid().ToString(),
                    Messages = new List<Message>
                    {
                        new Message
                        {
                            Text = "What are your plans for the weekend?",
                            GUID = Guid.NewGuid().ToString(),
                            Sender = "User5"
                        },
                        new Message
                        {
                            Text = "Not sure yet, maybe just relax.",
                            GUID = Guid.NewGuid().ToString(),
                            Sender = "User6"
                        }
                    }
                }},
                FirstName = "Karl",
                LastName = "Johanson",
                FriendList = new List<User>()

            });
        }
    }
}
