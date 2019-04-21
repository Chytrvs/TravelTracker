using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataModels
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        
        public List<Flight> TripFlights { get; set; }
    }
}