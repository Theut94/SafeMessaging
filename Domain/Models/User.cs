using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        required public string FirstName { get; set; }
        required public string LastName { get; set; }
        required public string GUID { get; set; }
        public List<User> FriendList { get; set; } = new List<User>();
        public List<Chat> Chats { get; set; } = new List<Chat>();
        public required Credentials Credentials { get; set; }
    }
}
