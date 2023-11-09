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
        public string Limit { get; set; }

        public RequestDto(String? Origin, String? Destination, String? Limit)
        {
            this.Origin = Origin;
            this.Destination = Destination;
            this.Limit = Limit;
        }
    }
}
