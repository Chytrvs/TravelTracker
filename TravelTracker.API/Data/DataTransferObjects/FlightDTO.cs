using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class FlightDTO
    {   
        [Required]
        [StringLength(4),MinLength(4)]
        //[RegularExpression("^[A-Z0-9]{3}$",ErrorMessage="Acronym is invalid, please use capital letters and numbers.")]
        public string DepartureAirportAcronym { get; set; }
        [Required]
        [StringLength(4),MinLength(4)]
        //[RegularExpression("^[A-Z0-9]{3}$",ErrorMessage="Acronym is invalid, please use capital letters and numbers.")]
        public string DestinationAirportAcronym { get; set; }
    }
}