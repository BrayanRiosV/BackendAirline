using AirlineApi.Dto;
using AirlineApi.Models.Interfaces;
using System;
using Newtonsoft.Json;

namespace AirlineApi.Models.Repositories
{

    public class JourneyRepository : IJourney
    {

        private IConfiguration Config { get; set; }
        private string UrlApi { get; set; }

        public JourneyRepository()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Config = builder.Build();
            UrlApi = Config["UrlApi"];
        }

        /// <summary>
        /// Retrieves a list of FlightsApiDto objects from the specified API endpoint.
        /// </summary>
        /// <returns>A list of FlightsApiDto objects representing flight information from the API.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the API request.</exception>
        /// <author>Brayan Rios</author>
        public async Task<List<FlightsApiDto>> ApiFlights()
        {
            try
            {
                List<FlightsApiDto> retorno = new List<FlightsApiDto>();

                using (HttpClient api = new HttpClient())
                {
                    HttpResponseMessage response = await api.GetAsync(UrlApi);

                    if (response.IsSuccessStatusCode)
                    {

                        string content = await response.Content.ReadAsStringAsync();

                        List<FlightsApiDto> flights = JsonConvert.DeserializeObject<List<FlightsApiDto>>(content);

                        retorno.AddRange(flights);
                        

                    }
                    else
                    {
                        Console.WriteLine($"Error en la petición: {response.StatusCode}");
                    }

                    return retorno;
                }
            }
            catch (Exception)
            {
                throw;
            }

            
            }


        }
}

