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

        [HttpGet("journey")]
        public async Task<ResultJourneyDto> GetJourney([FromQuery] string origin, [FromQuery] string destination, [FromQuery] int limit)
        {
            try
            {
                RequestDto request = new RequestDto(origin, destination, limit);

                ResultJourneyDto result = await App.GetJourneys(request);

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
