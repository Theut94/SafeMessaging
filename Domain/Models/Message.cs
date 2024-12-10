using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Message
    {
        public string? Text { get; set; }
        required public string GUID { get; set; }
        required public string Sender { get; set; }
    }
}
