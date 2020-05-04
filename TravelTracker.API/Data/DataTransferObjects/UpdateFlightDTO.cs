using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class UpdateFlightDTO
    {
        public string Description { get; set; }
        public string FlightDate { get; set; }
    }
}