using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using JRepo.Core;

namespace Praesidium.Data.Stores
{
    public class JRepoClientStore : IClientStore
    {
        private readonly IRepository<Client> _clientRepo;

        public JRepoClientStore(IRepository<Client> clientRepo)
        {
            _clientRepo = clientRepo;
        }
        
        public Task<Client> FindClientByIdAsync(string clientId)
        {
            return _clientRepo.GetOneAsync(it => it.ClientId == clientId);
        }
    }
}