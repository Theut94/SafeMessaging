﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Application.Util
{
    public class EncryptionUtil : IEncryptionUtil
    {
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
    }
}
