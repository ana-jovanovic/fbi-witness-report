using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WitnessReport.Configuration;
using WitnessReport.Models;
using WitnessReport.Services.Interfaces;

namespace WitnessReport.Services
{
    public class FbiDataService : IFbiDataService
    {
        private readonly IBaseHttpClient _httpClient;
        private readonly ILogger _logger;
        private readonly FbiConfiguration _configuration;

        public FbiDataService(IBaseHttpClient httpClient,
            ILogger<FbiDataService> logger,
            IOptions<FbiConfiguration> configuration)
        {
            _httpClient = httpClient;
            _logger = logger;
            _configuration = configuration.Value;
        }

        public async Task<Fugitive> GetMostWanted(string title)
        {
            Fugitive wanted = null;
            var shouldIterate = true;
            var page = 1;

            var parameters = new Dictionary<string, string>
            {
                { "page", $"{page}" },
                { "title", title }
            };

            try
            {
                while (shouldIterate)
                {
                    parameters.Remove("page");
                    parameters.Add("page", $"{page}");

                    var response =
                        await _httpClient.Get<MostWantedFugitives>(_configuration.Endpoint, null, parameters);

                    var fullName = parameters.FirstOrDefault(kvp => kvp.Key == "title").Value;

                    wanted = response.Fugitives.FirstOrDefault(f => f.Title.Equals(fullName)) ??
                             response.Fugitives.FirstOrDefault(f => f.Aliases.Contains(fullName));

                    if (response.Fugitives == null && !response.Fugitives.Any() || wanted != null)
                    {
                        shouldIterate = false;
                    }
                    else
                    {
                        page++;
                    }
                }

                return wanted;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Could not fetch most wanted fugitives: {ex}");
                return null;
            }
        }
    }
}
