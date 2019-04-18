using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data
{
    public class Point
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}