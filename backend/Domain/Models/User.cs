using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string GUID { get; set; }
        public required string PublicKey { get; set; }
        public List<Chat> Chats { get; set; } = new List<Chat>();
        public required Credentials Credentials { get; set; }
    }
}
