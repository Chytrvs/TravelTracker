using System;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class FlightForListDTO
    {
        public Airport FlightDepartureAirport { get; set; }
        public Airport FlightDestinationAirport { get; set; }
        public User User { get; set; }
        public String Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FlightDate { get; set; }
    }
}