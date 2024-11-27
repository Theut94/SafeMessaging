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
        private readonly IRepository<Credentials> _repo;

        public CredentialService(IRepository<Credentials> repo)
        {
            _repo = repo;
        }
    }
}
