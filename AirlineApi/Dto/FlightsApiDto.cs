namespace AirlineApi.Dto
{
    public class FlightsApiDto
    {

        public string departureStation { get; set; }

        public string arrivalStation { get; set; }

        public string flightCarrier { get; set; }

        public string flightNumber { get; set; }

        public double price { get; set; }

        public FlightsApiDto(string departureStation, string arrivalStation, string flightCarrier, string flightNumber, double price)
        {
            this.departureStation = departureStation;
            this.arrivalStation = arrivalStation;
            this.flightCarrier = flightCarrier;
            this.flightNumber = flightNumber;
            this.price = price;
        }
    }
}
