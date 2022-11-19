using IPGeolocation;
using Microsoft.Extensions.Logging;
using System;
using WitnessReports.Helpers;
using WitnessReports.Services.Interfaces;

namespace WitnessReports.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IIpAddressService _ipAddressService;
        private readonly ILogger _logger;

        public GeolocationService(IIpAddressService ipAddressService, ILogger<GeolocationService> logger)
        {
            _ipAddressService = ipAddressService;
            _logger = logger;
        }

        public Geolocation GetGeolocation()
        {
            var remoteIpAddress = _ipAddressService.GetRemoteIpAddress();

            if (string.IsNullOrEmpty(remoteIpAddress))
            {
                return null;
            }

            var geolocationApi = new IPGeolocationAPI(Constants.IpGeolocationApiKey);

            //var geolocationParams = new GeolocationParams();
            //geolocationParams.SetIPAddress("192.168.29.1");
            //geolocationParams.SetFields("ip");

            try
            {
                var geolocation = geolocationApi.GetGeolocation();
                return geolocation;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not retrieve geolocation with provided IP address: {ex}");
            }

            return null;
        }
    }
}
