using Newtonsoft.Json;

namespace WitnessReport.Models
{
    public class RequestData
    {
        [JsonRequired]
        public string FullName { get; set; }

        [JsonRequired]
        public string PhoneNumber { get; set; }
    }
}
