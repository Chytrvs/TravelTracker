using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class FlightResponseDTO
    {   
        [Required]
        public string Username { get; set; }
        [Required]
        public Airport DepartureAirport { get; set; }
        [Required]
        public Airport DestinationAirport { get; set; }
    }
}