using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interface
{
    public interface ICredentialService
    {
        public Task<Credentials> CreateCredentials(Credentials credentials);
        public Task<Credentials> GetCredentials(Credentials credentials);
        public Task<Credentials> DeleteCredentials(Credentials credentials);
    }
}
