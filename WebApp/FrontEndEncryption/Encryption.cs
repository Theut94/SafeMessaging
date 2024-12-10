using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace WebApp.FrontEndEncryption
{
    public class Encryption : IEncryptionUtil
    {
        public byte[] DeriveKeyFromPassword(string password, byte[] salt)
        {
            throw new NotImplementedException();
        }
        public byte[] HashPassword(string password, byte[] salt)
        {
            return Rfc2898DeriveBytes.Pbkdf2(
                        Encoding.UTF8.GetBytes(password),
                        salt,
                        iterations: 600_000,
                        hashAlgorithm: HashAlgorithmName.SHA256,
                        outputLength: 256 / 8
                    );
        }

        public byte[] GetSalt()
        {
            return Convert.FromBase64String(Crypto.GenerateSalt(32));
        }


    }
}
