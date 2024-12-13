using Application.Interface;
using Data.Repo.Interface;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CredentialService : ICredentialService
    {
        private readonly ICredentialRepository _repo;

        public CredentialService(ICredentialRepository repo)
        {
            _repo = repo;
        }

        public Task<Credentials> CreateCredentials(Credentials credentials)
        {

            throw new NotImplementedException(); /**
            if (_repo.GetAllAsync().Result.Any(x => x.User.GUID == credentials.User.GUID))
            {
                throw new InvalidOperationException("Credentials already exists");
            }
            _repo.AddAsync(credentials);
            return Task.FromResult(credentials);*/
        }

        public async Task<Credentials> GetCredentials(Credentials credentials)
        {
            throw new NotImplementedException();
            /**
            var credential = await _repo.GetByIdAsync(credentials.User.GUID);
            if (credential == null)
            {
                throw new InvalidOperationException("Credentials not found");
            }
            return credential;*/
        }

        public Task<Credentials> DeleteCredentials(Credentials credentials)
        {
            throw new NotImplementedException();
            /**
            var credentialToDelete = _repo.GetAllAsync().Result.FirstOrDefault(x => x.User.GUID == credentials.User.GUID);
            if (credentialToDelete == null)
            {
                throw new InvalidOperationException("Credentials not found");
            }
            _repo.DeleteAsync(credentialToDelete);
            return Task.FromResult(credentialToDelete);*/
        }
    }
}
