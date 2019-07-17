using Praesidium.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Praesidium.Data.Stores;

namespace Praesidium.Extensions
{
    public static class IdentityBuilderJRepoExtensions
    {
        public static IdentityBuilder AddJRepoStores(this IdentityBuilder identityBuilder)
        {
            identityBuilder.AddUserStore<JRepoUserStore>();
            identityBuilder.AddRoleStore<JRepoRoleStore>();
            
/*            identityBuilder.Services.TryAddScoped<IUserStore<AuctorUser>, JRepoUserStore>();
            identityBuilder.Services.TryAddScoped<IUserClaimStore<AuctorUser>, JRepoUserStore>();
            identityBuilder.Services.TryAddScoped<IUserLoginStore<AuctorUser>, JRepoUserStore>();
            identityBuilder.Services.TryAddScoped<IUserPasswordStore<AuctorUser>, JRepoUserStore>();
            identityBuilder.Services.TryAddScoped<IUserTwoFactorStore<AuctorUser>, JRepoUserStore>();
            identityBuilder.Services.TryAddScoped<IUserPhoneNumberStore<AuctorUser>, JRepoUserStore>();
            identityBuilder.Services.TryAddScoped<IUserEmailStore<AuctorUser>, JRepoUserStore>();
            identityBuilder.Services.TryAddScoped<IUserLockoutStore<AuctorUser>, JRepoUserStore>();*/

            return identityBuilder;
        }
    }
}