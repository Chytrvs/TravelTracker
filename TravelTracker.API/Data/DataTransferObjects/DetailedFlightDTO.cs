using System;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class DetailedFlightDTO
    {
        public Airport DepartureAirport { get; set; }
        public Airport DestinationAirport { get; set; }
        public DetailedUserDTO User { get; set; }
        public String Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FlightDate { get; set; }
    }
}