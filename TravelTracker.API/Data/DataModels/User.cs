using System.ComponentModel.DataAnnotations;

namespace TravelTracker.API.Data
{
    public class User
    {   [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
    }
}