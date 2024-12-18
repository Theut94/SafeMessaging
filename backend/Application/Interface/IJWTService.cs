using Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IJWTService
    {
        public string CreateTokenWithAttributes(User user);
        public string DecodeJWTString(string jwtString);
    }
}
