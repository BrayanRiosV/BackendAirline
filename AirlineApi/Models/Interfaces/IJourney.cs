using AirlineApi.Dto;

namespace AirlineApi.Models.Interfaces
{
    public interface IJourney
    {
        public Task<List<FlightsApiDto>> ApiFlights();
    }
}
