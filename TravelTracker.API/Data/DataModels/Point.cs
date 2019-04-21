using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data
{
    public class Point
    {   
        [Key]
        public int Id { get; set; }
        
        public double Latitude { get; set; }
        
        public double Longitude { get; set; }
    }
}