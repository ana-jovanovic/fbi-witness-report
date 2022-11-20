using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
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
            Fugitive wanted = new Fugitive();
            var page = 1;
            var pagesLeftToIterate = 1;
            var firstIteration = true;

            var parameters = new Dictionary<string, string>
            {
                { "page", $"{page}" },
                { "title", title }
            };

            try
            {
                while (pagesLeftToIterate != 0)
                {
                    parameters.Remove("page");
                    parameters.Add("page", $"{page}");

                    var response =
                        await _httpClient.Get<MostWantedFugitives>(_configuration.Endpoint, null, parameters);

                    var total = response.Total;
                    var count = response.Fugitives.Count;
                    if (count > 0 && firstIteration)
                    {
                        pagesLeftToIterate = total / count + (total % count != 0 ? 1 : 0) - 1;
                    }

                    var fullName = parameters.FirstOrDefault(kvp => kvp.Key == "title").Value;

                    wanted = response.Fugitives.FirstOrDefault(f => f.Title.Equals(fullName)) ??
                             response.Fugitives.FirstOrDefault(f => f.Aliases != null && f.Aliases.Any() && f.Aliases.Contains(fullName));

                    if (wanted != null)
                    {
                        return wanted;
                    }

                    pagesLeftToIterate--;
                    page++;

                    firstIteration = false;
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
