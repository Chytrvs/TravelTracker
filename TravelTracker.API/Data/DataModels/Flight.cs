using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataModels
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        
        public Point FlightStartingPoint { get; set; }
        
        public Point FlightEndingPoint { get; set; }
    }
}