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
                    City = "Test",
                    Country = "Test",
                    Zip = "0000",
                    CountryCode = "Test",
                    RegionName = "Test",
                    Region = "Test"
                });
        }
    }
}