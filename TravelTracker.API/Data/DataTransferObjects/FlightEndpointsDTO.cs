using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class FlightEndpointsDTO
    {   
        [Required]
        public Airport DepartureAirport { get; set; }
        [Required]
        public Airport DestinationAirport { get; set; }
    }
}