using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Util
{
    public interface IEncryptionUtil
    {
        byte[] DeriveKeyFromPassword(string password, byte[] salt);

        byte[] HashPassword(string password, byte[] salt);

        byte[] GetSalt();
    }
}
