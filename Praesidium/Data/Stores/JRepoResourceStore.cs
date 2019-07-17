using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using JRepo.Core;

namespace Praesidium.Data.Stores
{
    public class JRepoResourceStore : IResourceStore
    {
        private readonly IRepository<IdentityResource> _identityResourceRepository;
        private readonly IRepository<ApiResource> _apiResourceRepository;

        public JRepoResourceStore(IRepository<IdentityResource> identityResourceRepository, IRepository<ApiResource> apiResourceRepository)
        {
            _identityResourceRepository = identityResourceRepository;
            _apiResourceRepository = apiResourceRepository;
        }
        
        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return await _identityResourceRepository.GetAsync(it => it.UserClaims.Intersect(scopeNames).Any());
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return await _apiResourceRepository.GetAsync(it => it.UserClaims.Intersect(scopeNames).Any());
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            return _apiResourceRepository.GetOneAsync(it => it.Name == name);
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            return new Resources
            {
                ApiResources = await _apiResourceRepository.GetAsync(_ => true),
                IdentityResources = await _identityResourceRepository.GetAsync(_ => true)
            };
        }
    }
}