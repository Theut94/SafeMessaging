using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.FrontEndEncryption
{
    public interface IEncryptionUtil
    {
        byte[] DeriveKeyFromPassword(string password, byte[] salt);

        byte[] HashPassword(string password, byte[] salt);

        byte[] GetSalt();

        Message EncryptMessage(string text, byte[] commonSecret, Message message);

        string DecryptMessage(Message message, byte[] commonSecret);

    }
}
