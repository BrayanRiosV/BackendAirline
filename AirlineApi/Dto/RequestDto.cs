using System.Text.Json.Serialization;

namespace AirlineApi.Dto
{
    public class RequestDto
    {
        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        [JsonPropertyName("destination")]
        public string Destination { get; set; }

        [JsonPropertyName("limit")]
        public int? Limit { get; set; }

        public RequestDto(String? Origin, String? Destination,  int Limit)
        {
            this.Origin = Origin;
            this.Destination = Destination;
            this.Limit = Limit;
        }
    }
}
