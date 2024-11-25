using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Chat
    {
        public string GUID { get; set; }
        public List<Message> Messages { get; set; }
    }
}
