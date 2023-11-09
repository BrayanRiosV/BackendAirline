using AirlineApi.Dto;
using AirlineApi.Models.Aplication;
using Microsoft.AspNetCore.Mvc;

namespace AirlineApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AirlineController : Controller
    {

        public readonly JourneyAplication App;
        public AirlineController()
        {
            App = new JourneyAplication();
        }

        [HttpGet("api/journey")]
        public async Task<List<JourneyDto>> GetJourney([FromQuery] string origin, [FromQuery] string destination, [FromQuery] string limit)
        {
            try
            {
                RequestDto request = new RequestDto(origin, destination, limit);

                List<JourneyDto> result = await App.GetJourneys(request);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
