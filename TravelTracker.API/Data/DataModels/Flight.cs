using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace TravelTracker.API.Data.DataModels
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        
        public Airport DepartureAirport { get; set; }
        
        public Airport DestinationAirport { get; set; }

         public int UserId { get; set; }
         public User User { get; set; }
         public String Description { get; set; }
         public DateTime CreatedDate { get; set; }
        public DateTime FlightDate { get; set; }
    }
}