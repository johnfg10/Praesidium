using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using JRepo.Core;
using Praesidium.Models;

namespace Praesidium.Data.Stores
{
    public class JRepoDeviceFlowStore : IDeviceFlowStore
    {
        private readonly IRepository<JRepoDeviceFlow> _deviceFlowRepo;

        public JRepoDeviceFlowStore(IRepository<JRepoDeviceFlow> deviceFlowRepo)
        {
            _deviceFlowRepo = deviceFlowRepo;
        }
        
        public Task StoreDeviceAuthorizationAsync(string deviceCode, string userCode, DeviceCode data)
        {
            return _deviceFlowRepo.CreateAsync(new JRepoDeviceFlow(deviceCode, userCode, data));
/*            var rDeviceFlow = new JRepoDeviceFlow(deviceCode, userCode, data);
            var res = await _deviceFlowRepo.GetOneAsync(it => it == new JRepoDeviceFlow(deviceCode, userCode, data));
            if (res == null)
                await _deviceFlowRepo.CreateAsync(rDeviceFlow);
            
            await _deviceFlowRepo.UpdateAsync(it => it.UserCode)*/
        }

        public async Task<DeviceCode> FindByUserCodeAsync(string userCode)
        {
            return (await _deviceFlowRepo.GetOneAsync(it => it.UserCode == userCode)).Data;
        }

        public async Task<DeviceCode> FindByDeviceCodeAsync(string deviceCode)
        {
            return (await _deviceFlowRepo.GetOneAsync(it => it.DeviceCode == deviceCode)).Data;
        }

        public Task UpdateByUserCodeAsync(string userCode, DeviceCode data)
        {
            return _deviceFlowRepo.UpdateAsync(it => it.UserCode == userCode, new {Date = data});
        }

        public Task RemoveByDeviceCodeAsync(string deviceCode)
        {
            return _deviceFlowRepo.DeleteAsync(it => it.DeviceCode == deviceCode);
        }
    }
}