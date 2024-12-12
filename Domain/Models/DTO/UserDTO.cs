using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class UserDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string GUID { get; set; }
        public required byte[] PublickKey { get; set; }
        public List<User> FriendList { get; set; } = new List<User>();
        public List<Chat> Chats { get; set; } = new List<Chat>();
    }
}
