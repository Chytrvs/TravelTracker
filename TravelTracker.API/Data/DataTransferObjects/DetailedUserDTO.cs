using System;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class DetailedUserDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastActive { get; set; }
        public string Bio { get; set; }
        public Airport FavouriteAirport { get; set; }
    }
}