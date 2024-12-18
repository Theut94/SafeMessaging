using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class ChatDTO
    {
        public string GUID { get; set; }
        public List<Message>? Messages { get; set; }
    }
}
