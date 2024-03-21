using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public static class ApiResponseConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApiResponse>().HasKey(a => a.Id);

            modelBuilder.Entity<ApiResponse>().HasData(
                new ApiResponse
                {
                    Id = 1,
                    Ip = "0.0.0.0",
                    Hostname = "example.com",
                    Type = "ipv4",
                    City = "Test",
                    ContinentCode = "Test",
                    ContinentName = "Test",
                    CountryCode = "Test",
                    Zip = "0000",
                    CountryName = "Test",
                    RegionCode = "Test",
                    RegionName = "Test",
                });
        }
    }
}