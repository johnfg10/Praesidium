using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using JRepo.Core;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;
using Praesidium.Models;

namespace Praesidium.Data.Stores
{
    public class JRepoUserStore : IUserStore<AuctorUser>, IUserClaimStore<AuctorUser>, IUserLoginStore<AuctorUser>, IUserPasswordStore<AuctorUser>,
        IUserTwoFactorStore<AuctorUser>, IUserPhoneNumberStore<AuctorUser>, IUserEmailStore<AuctorUser>, 
        IUserLockoutStore<AuctorUser>, IUserSecurityStampStore<AuctorUser>, IUserRoleStore<AuctorUser>, IQueryableUserStore<AuctorUser>
    {
        private readonly IRepository<AuctorUser> _userRepository;
        private readonly IMongoCollection<AuctorUser> _userCollection;

        public JRepoUserStore(IRepository<AuctorUser> userRepository, IMongoCollection<AuctorUser> userCollection)
        {
            _userRepository = userRepository;
            _userCollection = userCollection;
        }
        
        public void Dispose()
        {
        }

        public Task<string> GetUserIdAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.Id);
        }

        public Task<string> GetUserNameAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.UserName);
        }

        public Task SetUserNameAsync(AuctorUser user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
            
            //return _userRepository.UpdateAsync(it => it.Id == user.Id, new {UserName = userName});
        }

        public Task<string> GetNormalizedUserNameAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult<string>(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(AuctorUser user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
            //return _userRepository.UpdateAsync(it => it.Id == user.Id, new {NormalizedName = normalizedName});
        }

        public async Task<IdentityResult> CreateAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            Console.WriteLine("Test create");
            await _userRepository.CreateAsync(user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            //user.Id = null;
           // var res = await _userRepository.GetOneAsync(it => it.Id == user.Id);
            
            await _userRepository.ReplaceOneAsync(it => it.Id == user.Id, user);
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(it => it.Id == user.Id);
            return IdentityResult.Success;
        }

        public Task<AuctorUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return _userRepository.GetOneAsync(it => it.Id == userId);
        }

        public Task<AuctorUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return _userRepository.GetOneAsync(it => it.NormalizedUserName == normalizedUserName);
        }

        public Task<IList<Claim>> GetClaimsAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserClaims as IList<Claim>);
        }

        public Task AddClaimsAsync(AuctorUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            //user.UserClaims = user.UserClaims.Concat(claims).ToArray();
            user.UserClaims.AddRange(claims);
            return Task.CompletedTask;
        }

        public  Task ReplaceClaimAsync(AuctorUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            user.UserClaims[user.UserClaims.IndexOf(claim)] = newClaim;
            return Task.CompletedTask;
        }

        public Task RemoveClaimsAsync(AuctorUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            foreach (var claim in claims)
            {
                user.UserClaims[user.UserClaims.IndexOf(claim)] = null;
            }

            //user.UserClaims.RemoveAll(claims.Contains);
            return Task.CompletedTask;
        }

        public async Task<IList<AuctorUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return (await _userRepository.GetAsync(it => it.UserClaims.Contains(claim)));
        }

        public Task AddLoginAsync(AuctorUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            user.UserLoginInfos.Add(login);
            return Task.CompletedTask;
        }

        public Task RemoveLoginAsync(AuctorUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            user.UserLoginInfos.RemoveAll(it => it.LoginProvider == loginProvider && it.ProviderKey == providerKey);
            return Task.CompletedTask;
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return user.UserLoginInfos;
        }

        public Task<AuctorUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return _userRepository.GetOneAsync(it => it.UserLoginInfos.FindIndex(ab => ab.LoginProvider == loginProvider && ab.ProviderKey == providerKey) > 0 );
        }

        public Task SetTwoFactorEnabledAsync(AuctorUser user, bool enabled, CancellationToken cancellationToken)
        {
            return _userRepository.UpdateAsync(it => it.Id == user.Id, new {TwoFactorEnabled = enabled});
        }

        public Task<bool> GetTwoFactorEnabledAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }

        public Task SetPhoneNumberAsync(AuctorUser user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;
            return Task.CompletedTask;
        }

        public Task<string> GetPhoneNumberAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(AuctorUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task SetEmailAsync(AuctorUser user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public  Task<bool> GetEmailConfirmedAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(AuctorUser user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.CompletedTask;
        }

        public Task<AuctorUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return _userRepository.GetOneAsync(it => it.NormalizedEmail == normalizedEmail);
        }

        public Task<string> GetNormalizedEmailAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(AuctorUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            return Task.CompletedTask;
        }

        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return user.LockoutEnd;
        }

        public Task SetLockoutEndDateAsync(AuctorUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            user.LockoutEnd = lockoutEnd;
            return Task.CompletedTask;
        }

        public Task<int> IncrementAccessFailedCountAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.AccessFailedCount++);
/*            var lockOut =  (await _userRepository.GetOneAsync(it => it.Id == user.Id)).AccessFailedCount;
            lockOut++;
            await _userRepository.UpdateAsync(it => it.Id == user.Id, new { AccessFailedCount=lockOut });
            return lockOut;*/
        }

        public Task ResetAccessFailedCountAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            user.AccessFailedCount = 0;
            return Task.CompletedTask;
        }

        public Task<int> GetAccessFailedCountAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.AccessFailedCount);
        }

        public Task<bool> GetLockoutEnabledAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            
            return Task.FromResult<bool>(user.LockoutEnabled);
            //return Task.FromResult(user.LockoutEnabled);
        }

        public Task SetLockoutEnabledAsync(AuctorUser user, bool enabled, CancellationToken cancellationToken)
        {
            user.LockoutEnabled = enabled;
            return Task.CompletedTask;
            //return _userRepository.UpdateAsync(it => it.Id == user.Id, new { LockoutEnabled=enabled });
        }

        public Task SetPasswordHashAsync(AuctorUser user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
            //return _userRepository.UpdateAsync(it => it.Id == user.Id, new { PasswordHash=passwordHash });
        }

        public Task<string> GetPasswordHashAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public  Task<bool> HasPasswordAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(string.IsNullOrWhiteSpace(user.PasswordHash));
        }

        public Task AddToRoleAsync(AuctorUser user, string roleName, CancellationToken cancellationToken)
        {
            user.UserRoles.Add(roleName);
            return Task.CompletedTask;
        }

        public Task RemoveFromRoleAsync(AuctorUser user, string roleName, CancellationToken cancellationToken)
        {
            user.UserRoles.Remove(roleName);
            return Task.CompletedTask;
        }

        public Task<IList<string>> GetRolesAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult<IList<string>>(user.UserRoles);
        }

        public Task<bool> IsInRoleAsync(AuctorUser user, string roleName, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserRoles.Contains(roleName));
        }

        public async Task<IList<AuctorUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return (await _userRepository.GetAsync(it => it.UserRoles.Contains(roleName)));
        }

        public Task SetSecurityStampAsync(AuctorUser user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            return Task.CompletedTask;
        }

        public Task<string> GetSecurityStampAsync(AuctorUser user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.SecurityStamp);
        }

        public IQueryable<AuctorUser> Users => _userCollection.AsQueryable();
    }
}