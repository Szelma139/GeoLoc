using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ApiResponse> ApiResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApiResponseConfiguration.Configure(modelBuilder);
        }
    }
}
