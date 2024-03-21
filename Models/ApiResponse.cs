namespace Backend.Models
{
    public class ApiResponse
    {
        public long Id { get; set; }
        public string Ip { get; set; }
        public string Hostname { get; set; }
        public string Type { get; set; }
        public string ContinentCode { get; set; }
        public string ContinentName { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }

    }
}
