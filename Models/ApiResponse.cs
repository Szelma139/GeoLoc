using System.Text.Json.Serialization;

namespace Backend.Models
{
    public class ApiResponse
    {
        public long Id { get; set; }

        [JsonPropertyName("query")]
        public string Ip { get; set; }

        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string RegionName { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

    }
}
