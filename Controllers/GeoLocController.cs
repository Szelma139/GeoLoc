using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeoLocController : ControllerBase
    {

        private readonly ILogger<GeoLocController> _logger;
        private readonly IGeoLocService _geoLocationService;

        public GeoLocController(ILogger<GeoLocController> logger, IGeoLocService geoLocationService)
        {
            _logger = logger;
            _geoLocationService = geoLocationService;
        }

        [HttpGet]
        [Route("{ip}")]
        public Task<ApiResponse> GetByIp(string ip)
        {
            var t = _geoLocationService.GetGeoDataByIp(ip);
            return t;
        }
    }
}
