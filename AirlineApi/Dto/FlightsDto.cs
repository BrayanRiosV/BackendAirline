namespace AirlineApi.Dto
{
    public class FlightsDto
    {

        public TransportDto transport { get; set; }

        public string Origin { get; set; }

        public string Destination { get; set; }

        public double Price { get; set; }

        public FlightsDto(TransportDto transport, string origin, string destination, double price)
        {
            this.transport = transport;
            Origin = origin;
            Destination = destination;
            Price = price;
        }
    }
}
