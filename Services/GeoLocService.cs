using Backend.Models;
using Backend.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Backend.Services
{
    public interface IGeoLocService
    {
        public Task<ApiResponse> GetGeoDataByIp(string ip);
    }
    public class GeoLocService : IGeoLocService
    {
        private readonly IOptions<ExternalApiOptions> _externalApiOptions;
        private readonly AppDbContext _dbContext;
        private readonly IHttpClientFactory _httpClientFactory;
        public GeoLocService(IOptions<ExternalApiOptions> externalApiOptions, AppDbContext dbContext, IHttpClientFactory httpClientFactory)
        {
            _externalApiOptions = externalApiOptions;
            _dbContext = dbContext;
            _httpClientFactory = httpClientFactory;
        }

        private async Task<ApiResponse?> GetGeoDataFromExternalApiByIp(string ip)
        {
            var client = _httpClientFactory.CreateClient("ip-api");
            var res = await client.GetFromJsonAsync<ApiResponse>($"/json/{ip}"); //externalApiOptions.GetReqPath

            return res;
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
                result = await GetGeoDataFromExternalApiByIp(ip);
                await AddExternalResponseToLocalDb(result);
            }
            return result;

        }
    }
}

