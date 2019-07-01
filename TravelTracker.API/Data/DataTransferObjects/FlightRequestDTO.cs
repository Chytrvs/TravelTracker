using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class FlightRequestDTO
    {   
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(4),MinLength(4)]
        public string DepartureAirportAcronym { get; set; }
        [Required]
        [StringLength(4),MinLength(4)]
        public string DestinationAirportAcronym { get; set; }
    }
}