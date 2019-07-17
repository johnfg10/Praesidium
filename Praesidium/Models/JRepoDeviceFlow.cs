using IdentityServer4.Models;

namespace Praesidium.Models
{
    public class JRepoDeviceFlow
    {
        public JRepoDeviceFlow(string deviceCode, string userCode, DeviceCode data)
        {
            DeviceCode = deviceCode;
            UserCode = userCode;
            Data = data;
        }

        public string DeviceCode { get; set; }
        public string UserCode { get; set; }
        public DeviceCode Data { get; set; }
    }
}