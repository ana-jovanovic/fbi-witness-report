using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using WitnessReport.Configuration;
using WitnessReport.Services.Interfaces;

namespace WitnessReport.Services
{
    public class FBIDataService : IFBIDataService
    {
        private readonly IBaseHttpClient httpClient;
        private readonly ILogger _logger;
        private readonly FBIConfiguration _configuration;

        public FBIDataService(IBaseHttpClient httpClient,
            ILogger<FBIDataService> logger,
            IOptions<FBIConfiguration> configuration)
        {
            _logger = logger;
            _configuration = configuration.Value;
        }

        public async Task GetMostWanted()
        {
            try
            {
                var hc = new System.Net.Http.HttpClient();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
