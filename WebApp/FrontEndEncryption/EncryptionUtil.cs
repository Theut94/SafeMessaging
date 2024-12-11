using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace WebApp.FrontEndEncryption
{
    public class EncryptionUtil : IEncryptionUtil
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

        public Message EncryptMessage( string text , byte[] commonSecret, Message message)
        {
            using(Aes aes =  Aes.Create())
            {
                aes.Key = commonSecret;
                aes.GenerateIV();

                byte[] iv = aes.IV;
                byte[] encrypted;

                using (var encryptor = aes.CreateEncryptor())
                {
                    byte[] messageBytes = Encoding.UTF8.GetBytes(text);
                    message.Text = encryptor.TransformFinalBlock(messageBytes, 0, messageBytes.Length);
                }
                message.IV = iv;
            }
            return message;
        }

        public string DecryptMessage(Message message, byte[] commonSecret)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = commonSecret;
                aes.IV = message.IV; // Use the same IV used during encryption

                byte[] decrypted;
                using (var decryptor = aes.CreateDecryptor())
                {
                    decrypted = decryptor.TransformFinalBlock(message.Text, 0, message.Text.Length);
                }

                return Encoding.UTF8.GetString(decrypted); // Convert decrypted bytes back to string
            }
        }
    }
}
