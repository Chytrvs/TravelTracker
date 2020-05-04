using System;

namespace TravelTracker.API.Helpers
{
    public static class FlightDateResolver
    {
        public static DateTime ResolveFlightDate(string date){
            DateTime FlightDateConverted;
            return (DateTime.TryParse(date,out FlightDateConverted))?FlightDateConverted:DateTime.UtcNow;
        }
    }
}