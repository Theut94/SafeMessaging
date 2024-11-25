using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Message
    {
        public string Text { get; set; }
        public string GUID { get; set; }
        public string Sender { get; set; }
    }
}
