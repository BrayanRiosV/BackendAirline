namespace AirlineApi.Dto
{
    public class TemporalJourney
    {

        public List<FlightsDto> Flights { get; set; }

        public List<FlightsApiDto> TemporalFlights { get; set; }
        public string Origin { get; set; }

        public string Destination { get; set; }

        public double Price { get; set; }

        public TemporalJourney(List<FlightsDto> flights, List<FlightsApiDto> temporalFlights, string origin, string destination, double price)
        {
            Flights = flights;
            TemporalFlights = temporalFlights;
            Origin = origin;
            Destination = destination;
            Price = price;
        }

    }
}
