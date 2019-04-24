using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataModels
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        
        public Airport FlightDepartureAirport { get; set; }
        
        public Airport FlightDestinationAirport { get; set; }

         public int UserId { get; set; }
         public User User { get; set; }
    }
}