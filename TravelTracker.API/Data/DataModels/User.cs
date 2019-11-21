using System;
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
        public List<Flight> UserFlights { get; set; }=new List<Flight>();
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }
        public string Bio { get; set; }
        public Airport FavouriteAirport { get; set; }
    }
}