using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string GUID { get; set; }
        public Credentials Credentials { get; set; }
        public List<User> FriendList { get; set; } = new List<User>();
        public List<Chat> Chats { get; set; } = new List<Chat>();

    }
}
