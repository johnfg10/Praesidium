using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using JRepo.Core;

namespace Praesidium.Data.Stores
{
    public class JRepoPersistedGrantStore : IPersistedGrantStore
    {
        private readonly IRepository<PersistedGrant> _grantRepository;

        public JRepoPersistedGrantStore(IRepository<PersistedGrant> grantRepository)
        {
            _grantRepository = grantRepository;
        }
        
        public async Task StoreAsync(PersistedGrant grant)
        {
            var res = await _grantRepository.GetOneAsync(it => it == grant);
            if (res == null)
                await _grantRepository.CreateAsync(grant);
            else
                await _grantRepository.UpdateAsync(it => it == grant, grant);
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            return _grantRepository.GetOneAsync(it => it.Key == key);
        }

        public async Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return await _grantRepository.GetAsync(it => it.SubjectId == subjectId);
        }

        public Task RemoveAsync(string key)
        {
            return _grantRepository.DeleteAsync(it => it.Key == key);
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            return _grantRepository.DeleteAsync(it => it.SubjectId == subjectId && it.ClientId == clientId);
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            return _grantRepository.DeleteAsync(it => it.SubjectId == subjectId && it.ClientId == clientId && it.Type == type);
        }
    }
}