using AirlineApi.Dto;
using AirlineApi.Models.Repositories;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace AirlineApi.Models.Aplication
{
    public class JourneyAplication
    {

        private JourneyRepository Repository { get; set; }

        public JourneyAplication()
        {
            Repository = new JourneyRepository();
        }

        /// <summary>
        /// Retrieves a list of JourneyDto objects based on the given request, sorting and calculating flight combinations.
        /// </summary>
        /// <param name="request">Request information, including origin and destination.</param>
        /// <returns>A list of JourneyDto objects representing sorted flight combinations.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the process.</exception>
        /// <author>Brayan Rios</author>
        public async Task<List<JourneyDto>> GetJourneys(RequestDto request)
        {
            try
            {
                List<FlightsApiDto> flights = await Repository.ApiFlights();

                List<FlightsApiDto> originFlights = flights.Where(flight => flight.departureStation == request.Origin).ToList();

                flights.RemoveAll(flight => flight.departureStation == request.Origin);

                List<JourneyDto> retorno = await SortJourney(flights, originFlights, request);

                return retorno;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Sorts and calculates all possible flight combinations based on given origins and a destination request.
        /// </summary>
        /// <param name="flights">List of all available flights.</param>
        /// <param name="origins">List of flight information for different origins.</param>
        /// <param name="request">Request information, including origin and destination.</param>
        /// <returns>A list of JourneyDto objects representing sorted flight combinations.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the process.</exception>
        /// <author>Brayan Rios</author>
        public async Task<List<JourneyDto>> SortJourney(List<FlightsApiDto> flights, List<FlightsApiDto> origins, RequestDto request)
        {
            try
            {
                List<JourneyDto> journeys = new List<JourneyDto>();
                foreach (var origin in origins)
                {
                    List<FlightsDto> newFlights = new List<FlightsDto>();
                    if (request.Destination == origin.arrivalStation)
                    {
                        TransportDto transport = new TransportDto(origin.flightCarrier, origin.flightNumber);
                        FlightsDto fly = new FlightsDto(transport, origin.departureStation, origin.arrivalStation, origin.price);
                        newFlights.Add(fly);

                        JourneyDto journey = new JourneyDto(newFlights, request.Origin, request.Destination, origin.price);
                        journeys.Add(journey);
                    }
                    else
                    {

                        List<JourneyDto> resultJourneys = OrderJourney(origin, flights, request);

                        journeys.AddRange(resultJourneys);

                    }

                }

                return journeys;
            }
            catch (Exception)
            {

                throw;
            }
        }


        /// <summary>
        /// Calculates all possible flight combinations for traveling from an origin to a destination.
        /// </summary>
        /// <param name="origin">Information about the origin flight.</param>
        /// <param name="flights">List of all available flights.</param>
        /// <param name="request">Request information, including origin and destination.</param>
        /// <returns>A list of JourneyDto objects representing all possible flight combinations.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the process.</exception>
        /// <author>Brayan Rios</author>
        static List<JourneyDto> OrderJourney(FlightsApiDto origin, List<FlightsApiDto> flights, RequestDto request)
        {
            try
            {
                TransportDto transport = new TransportDto(origin.flightCarrier, origin.flightNumber);
                FlightsDto fly = new FlightsDto(transport, origin.departureStation, origin.arrivalStation, origin.price);
                List<FlightsDto> newFlights = new List<FlightsDto>();
                newFlights.Add(fly);
                TemporalJourney journey = new TemporalJourney(newFlights, flights, request.Origin, request.Destination, origin.price);

                Queue<TemporalJourney> queue = new Queue<TemporalJourney>();

                queue.Enqueue(journey);

                List<JourneyDto> journeys = new List<JourneyDto>();


                while (queue.Count > 0)
                {
                    TemporalJourney journeyTemp = queue.Dequeue();

                    FlightsDto flight = journeyTemp.Flights[journeyTemp.Flights.Count - 1];

                    if (flight.Destination != request.Destination)
                    {
                        journeyTemp.TemporalFlights.RemoveAll(flightTemp => flightTemp.arrivalStation == flight.Origin);

                        var directFlights = journeyTemp.TemporalFlights.Where(flightTemp => flightTemp.departureStation == flight.Destination).ToList();

                        journeyTemp.TemporalFlights.RemoveAll(flightTemp => flightTemp.departureStation == flight.Destination || flightTemp.arrivalStation == flight.Origin);

                        if (directFlights.Count > 0)
                        {
                            foreach (var directFlight in directFlights)
                            {
                                if (directFlight.arrivalStation == journeyTemp.Destination)
                                {
                                    TransportDto tempTransport = new TransportDto(directFlight.flightCarrier, directFlight.flightNumber);
                                    FlightsDto tempFly = new FlightsDto(transport, directFlight.departureStation, directFlight.arrivalStation, directFlight.price);

                                    journeyTemp.Flights.Add(tempFly);

                                    JourneyDto tempJourney = new JourneyDto(new List<FlightsDto>(journeyTemp.Flights), request.Origin, request.Destination, journeyTemp.Price + directFlight.price);

                                    journeys.Add(tempJourney);

                                    journeyTemp.Flights.Remove(tempFly);
                                }
                                else
                                {
                                    TransportDto tempTransport = new TransportDto(directFlight.flightCarrier, directFlight.flightNumber);
                                    FlightsDto tempFly = new FlightsDto(transport, directFlight.departureStation, directFlight.arrivalStation, directFlight.price);
                                    journeyTemp.Flights.Add(tempFly);

                                    journeyTemp.Price += directFlight.price;

                                    queue.Enqueue(new TemporalJourney(new List<FlightsDto>(journeyTemp.Flights), journeyTemp.TemporalFlights, request.Origin, request.Destination, journeyTemp.Price));

                                    journeyTemp.Price -= directFlight.price;
                                    journeyTemp.Flights.Remove(tempFly);
                                }
                            }
                        }
                    }                                 
                }

                return journeys;
            }
            catch (Exception)
            {

                throw;
            }

        }


    }
}
