using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.DTO
{
    public class LoginUserDTO
    {
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public byte[] Salt { get; set; }

    }
}
