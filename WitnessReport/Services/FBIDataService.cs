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
            Fugitive wanted = new Fugitive();
            var page = 1;
            var parameters = new Dictionary<string, string>
            {
                { "page", $"{page}" },
                { "title", title }
            };

            while (page != 0)
            {
                parameters.Remove("page");
                parameters.Add("page", $"{page}");

                var response = await _httpClient.Get<MostWantedFugitives>(_configuration.Endpoint, null, parameters);

                wanted = response.Fugitives.FirstOrDefault(f =>
                             f.Title.Equals(parameters.FirstOrDefault(kvp => kvp.Key == "title").Value)) ??
                         response.Fugitives.FirstOrDefault(f =>
                             f.Aliases.Contains(parameters.FirstOrDefault(kvp => kvp.Key == "title").Value));

                if (response.Fugitives == null && !response.Fugitives.Any() || wanted != null)
                {
                    page = 0;
                }
                else
                {
                    page++;
                }
            }

            return wanted;
        }
    }
}
