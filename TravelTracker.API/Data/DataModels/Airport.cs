using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data
{
    public class Airport
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Acronym { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
    }
}