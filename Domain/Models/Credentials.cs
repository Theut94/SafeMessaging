using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Credentials
    {
        required public string Password { get; set; }
        required public string UserName { get; set; } // email
        required public User User { get; set; }
    }
}
