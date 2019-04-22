using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataModels
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        
        public Airport FlightStartingPoint { get; set; }
        
        public Airport FlightEndingPoint { get; set; }
    }
}