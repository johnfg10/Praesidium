using Microsoft.Extensions.DependencyInjection;
using Praesidium.Data.Stores;

namespace Praesidium.Extensions
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddJRepoStores(this IIdentityServerBuilder builder)
        {
            builder.AddClientStore<JRepoClientStore>();
            builder.AddDeviceFlowStore<JRepoDeviceFlowStore>();
            builder.AddPersistedGrantStore<JRepoPersistedGrantStore>();
            builder.AddResourceStore<JRepoResourceStore>();
            return builder;
        } 
    }
}