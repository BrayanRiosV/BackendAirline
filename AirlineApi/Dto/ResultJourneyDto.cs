namespace AirlineApi.Dto
{
    public class ResultJourneyDto
    {

        public List<JourneyDto> Journeys { get; set; }

        public string Message { get; set; }

        public string Status { get; set; }


        public ResultJourneyDto(List<JourneyDto> journeyDto, string message, string status)
        {
            Journeys = journeyDto;
            Message = message;
            Status = status;
        }


    }
}
