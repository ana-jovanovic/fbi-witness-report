using Newtonsoft.Json;

namespace WitnessReports.Models
{
    public class RequestData
    {
        [JsonRequired]
        public string FullName { get; set; }

        [JsonRequired]
        public string PhoneNumber { get; set; }
    }
}
