using Microsoft.Extensions.Logging;
using PhoneNumbers;
using System;
using WitnessReport.Services.Interfaces;

namespace WitnessReport.Services
{
    public class PhoneNumberValidityService : IPhoneNumberValidityService
    {
        private readonly IGeolocationService _geolocationService;
        private readonly ILogger _logger;

        public PhoneNumberValidityService(IGeolocationService geolocationService, ILogger<PhoneNumberValidityService> logger)
        {
            _geolocationService = geolocationService;
            _logger = logger;
        }

        public bool IsPhoneNumberValid(string phoneNumber)
        {
            var phoneNumberUtil = PhoneNumberUtil.GetInstance();
            try
            {
                var geolocation = _geolocationService.GetGeolocation();
                if (geolocation == null)
                {
                    return false;
                }

                var countryCode = geolocation.GetCountryCode2();
                var paredPhoneNumber = phoneNumberUtil.Parse(phoneNumber, countryCode);
                return phoneNumberUtil.IsValidNumberForRegion(paredPhoneNumber, countryCode);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not parse phone number: {ex}");
                return false;
            }
        }
    }
}
