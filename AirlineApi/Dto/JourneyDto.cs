namespace AirlineApi.Dto
{
    public class JourneyDto
    {

        public List<FlightsDto> Flights { get; set; }


        public string Origin { get; set; }

        public string Destination { get; set; }

        public double Price { get; set; }

        public JourneyDto(List<FlightsDto> flights, string origin, string destination, double price)
        {
            Flights = flights;
            Origin = origin;
            Destination = destination;
            Price = price;
        }
    }
}
