using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WitnessReport.Models
{
    [Serializable]
    public class MostWantedFugitives
    {
        [JsonProperty("total")]
        public int Total { get; set; }

        [JsonProperty("items")]
        public IList<Fugitive> Fugitives { get; set; }

        [JsonProperty("page")]
        public int Page { get; set; }
    }
}
