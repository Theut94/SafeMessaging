using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class MessageDTO
    {
        public byte[]? Text { get; set; }
        public byte[]? IV { get; set; }
        public string Sender { get; set; }
    }
}
