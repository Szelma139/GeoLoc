using Backend.Models;
using Backend.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Backend
{
    public interface IGeoLocService
    {
        public Task<ApiResponse> GetGeoDataByIp(string ip);
    }
    public class GeoLocService : IGeoLocService
    {
        private readonly IOptions<ExternalApiIpstackOptions> _externalApiOptions;
        private readonly AppDbContext _dbContext;

        public GeoLocService(IOptions<ExternalApiIpstackOptions> externalApiOptions, AppDbContext dbContext)
        {
            _externalApiOptions = externalApiOptions;
            _dbContext = dbContext;
        }

        private async Task<ApiResponse> GetGeoDataFromIpstackByIp(string ip)
        {
            HttpClient client = HttpClientFactory.GetInstance(_externalApiOptions.Value);
            try
            {
                HttpResponseMessage response = await client.GetAsync(_externalApiOptions.Value.ApiPath);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Error while querying external api HTTP: {e.Message}");
                throw;
            }
        }

        private async Task<ApiResponse> GetGeoDataFromLocalByIp(string ip)
        {
            return await _dbContext.ApiResponses.FirstOrDefaultAsync(x => x.Ip == ip);
        }

        public async Task AddExternalResponseToLocalDb(ApiResponse response)
        {
            if (response != null)
            {
                _dbContext.ApiResponses.Add(response);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<ApiResponse> GetGeoDataByIp(string ip)
        {
            var validator = new IpAddressValidator();
            var validationResult = validator.Validate(ip);
            if (!validationResult.IsValid)
            {
                throw new ArgumentException(validationResult.Errors.First().ErrorMessage);
            }

            var result = await GetGeoDataFromLocalByIp(ip);
            if (result == null)
            {
                result = await GetGeoDataFromIpstackByIp(ip);
                await AddExternalResponseToLocalDb(result);
            }
            return result;

        }
    }
}

