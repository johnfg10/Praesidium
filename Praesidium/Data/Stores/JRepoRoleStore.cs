using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using JRepo.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Praesidium.Models;

namespace Praesidium.Data.Stores
{
    public class JRepoRoleStore : IRoleStore<AuctorRole>
    {
        private readonly IRepository<AuctorRole> _roleRepository;

        public JRepoRoleStore(IRepository<AuctorRole> roleRepository)
        {
            _roleRepository = roleRepository;
        }
        
        public void Dispose()
        {
        }

        public async Task<IdentityResult> CreateAsync(AuctorRole role, CancellationToken cancellationToken)
        {
            await _roleRepository.CreateAsync(role);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(AuctorRole role, CancellationToken cancellationToken)
        {
            await _roleRepository.UpdateAsync(it => it.Id == role.Id, role);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(AuctorRole role, CancellationToken cancellationToken)
        {
            await _roleRepository.DeleteAsync(it => it.Id == role.Id);
            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(AuctorRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(AuctorRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(AuctorRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedRoleNameAsync(AuctorRole role, CancellationToken cancellationToken)
        {

            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(AuctorRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task<AuctorRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return _roleRepository.GetOneAsync(it => it.Id == Guid.Parse(roleId));
        }

        public Task<AuctorRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        { 
            return _roleRepository.GetOneAsync(it => it.NormalizedName == normalizedRoleName);
        }
    }
}