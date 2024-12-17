using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class MessageDTO
    {
        public string? Text { get; set; }
        public string? IV { get; set; }
        public string Sender { get; set; }
    }
}
