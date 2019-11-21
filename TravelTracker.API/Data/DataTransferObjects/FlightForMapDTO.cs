using System;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class FlightForMapDTO
    {
        public string Username { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport DestinationAirport { get; set; }
        public DateTime FlightDate { get; set; }
    }
}