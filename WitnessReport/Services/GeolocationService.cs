using IPGeolocation;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using WitnessReport.Configuration;
using WitnessReport.Services.Interfaces;

namespace WitnessReport.Services
{
    public class GeolocationService : IGeolocationService
    {
        private readonly IIpAddressService _ipAddressService;
        private readonly ILogger _logger;
        private readonly IpGeolocationConfiguration _configuration;

        public GeolocationService(IIpAddressService ipAddressService,
            ILogger<GeolocationService> logger,
            IOptions<IpGeolocationConfiguration> configuration)
        {
            _ipAddressService = ipAddressService;
            _logger = logger;
            _configuration = configuration.Value;
        }

        public Geolocation GetGeolocation()
        {
            var remoteIpAddress = _ipAddressService.GetRemoteIpAddress();

            if (string.IsNullOrEmpty(remoteIpAddress))
            {
                return null;
            }

            var geolocationApi = new IPGeolocationAPI(_configuration.ApiKey);

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
