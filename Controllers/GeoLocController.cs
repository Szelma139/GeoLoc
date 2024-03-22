using Backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GeoLocController : ControllerBase
    {
        private readonly IGeoLocService _geoLocationService;

        public GeoLocController(IGeoLocService geoLocationService)
        {
            _geoLocationService = geoLocationService;
        }

        [HttpGet]
        [Route("{ip}")]
        public async Task<IActionResult> GetByIp(string ip)
        {
            var res = await _geoLocationService.GetGeoDataByIp(ip);
            if (res == null)
            {
                return NotFound();
            }

            return Ok(res);
        }
    }
}
