using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TravelTracker.API.Data.DataModels;

namespace TravelTracker.API.Data
{
    public class User
    {   [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        public List<Trip> UserTrips { get; set; }
    }
}