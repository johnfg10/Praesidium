using IdentityServer4.Models;
using JRepo.AspNet;
using JRepo.MongoDb.AspNet;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using Praesidium.Models;

namespace Praesidium.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddJRepoMongoDb(this IServiceCollection serviceCollection, MongoOptions mongoOptions)
        {
            serviceCollection
                .TryAddSingleton<IMongoClient>(it => new MongoClient(mongoOptions.MongoDbConnectionString));
            serviceCollection.TryAddSingleton<IMongoDatabase>(it =>
                it.GetRequiredService<IMongoClient>().GetDatabase(mongoOptions.DatabaseName));
            
            serviceCollection.AddMongoRepo<Client>("ClientStore");
            serviceCollection.AddMongoRepo<JRepoDeviceFlow>("DeviceFlowStore");
            serviceCollection.AddMongoRepo<PersistedGrant>("PersistedGrantStore");
            serviceCollection.AddMongoRepo<IdentityResource>("IdentityResourceStore");
            serviceCollection.AddMongoRepo<ApiResource>("ApiResourceStore");
            serviceCollection.AddMongoRepo<AuctorRole>("AuctorRoleStore");
            serviceCollection.AddMongoRepo<AuctorUser>("AuctorUserStore");

            return serviceCollection;

/*            serviceCollection.AddScoped<IMongoCollection<AuctorUser>>(it => it.GetRequiredService<IMongoDatabase>().GetCollection<AuctorUser>(mongoIdentityOptions.ClientCollection));
            serviceCollection.AddScoped<IMongoCollection<AdvehoIdentityResource>>(it => it.GetRequiredService<IMongoDatabase>().GetCollection<AdvehoIdentityResource>(mongoIdentityOptions.IdentityResourceCollection));
            serviceCollection.AddScoped<IMongoCollection<AdvehoApiResource>>(it => it.GetRequiredService<IMongoDatabase>().GetCollection<AdvehoApiResource>(mongoIdentityOptions.ApiResourceCollection));
            
            serviceCollection.AddMongoDbJRepo<Client>();
            serviceCollection.AddMongoDbJRepo<JRepoDeviceFlow>();
            serviceCollection.AddMongoDbJRepo<PersistedGrant>();
            serviceCollection.AddMongoDbJRepo<IdentityResource>();
            serviceCollection.AddMongoDbJRepo<ApiResource>();
            serviceCollection.AddMongoDbJRepo<AuctorRole>();
            serviceCollection.AddMongoDbJRepo<AuctorUser>();*/
        }

        public static IServiceCollection AddMongoRepo<T>(this IServiceCollection serviceCollection, string CollectionName) where T : class
        {
            serviceCollection.AddScoped<IMongoCollection<T>>(it => it.GetRequiredService<IMongoDatabase>().GetCollection<T>(CollectionName));
            serviceCollection.AddMongoDbJRepo<T>();
            return serviceCollection;
        }
    }
}