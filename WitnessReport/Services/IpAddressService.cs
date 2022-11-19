using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using WitnessReports.Services.Interfaces;

namespace WitnessReports.Services
{
    public class IpAddressService : IIpAddressService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IpAddressService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetRemoteIpAddress()
        {
            var remoteIpAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;

            if (remoteIpAddress == null) return string.Empty;

            // If we got an IPV6 address, then we need to ask the network for the IPV4 address 
            // This usually only happens when the browser is on the same machine as the server.
            if (remoteIpAddress.AddressFamily == AddressFamily.InterNetworkV6)
            {
                remoteIpAddress = Dns.GetHostEntry(remoteIpAddress).AddressList
                    .First(x => x.AddressFamily == AddressFamily.InterNetwork);
            }

            return remoteIpAddress.ToString();
        }
    }
}
