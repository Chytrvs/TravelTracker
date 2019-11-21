using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data.DataTransferObjects
{
    public class RegisterUserDTO
    {   
        [Required]
        [StringLength(20,MinimumLength=3,ErrorMessage="Username has to be between 3 and 20 characters.")]
        public string Username { get; set; }
        [Required]
        [StringLength(64,MinimumLength=6,ErrorMessage="Username has to be between 6 and 64 characters.")]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [StringLength(64)]
        public string Email { get; set; }
        public Airport FavouriteAirport { get; set; }
    }
}