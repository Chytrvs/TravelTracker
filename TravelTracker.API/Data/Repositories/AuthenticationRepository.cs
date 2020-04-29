using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Data.Repositories
{
    /// <summary>
    /// Implements db queries that allow users to authenticate.
    /// </summary>
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly TravelTrackerDbContext _context;
        private readonly IUserRepository _userRepo;


        public AuthenticationRepository(TravelTrackerDbContext context, IUserRepository userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }

        /// <summary>
        /// Executes database query that verifies if provided user exists, then it uses VerifyUser method to check if provided password and login are matching.
        /// If both conditions are met, it returns users data, otherwise returns null
        /// </summary>
        public async Task<User> LoginUser(string username, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
            if (!VerifyUser(password, user))
            {
                return null;
            }
            else
            {
                await UpdateUserLastActiveDate(user);
                return user;
            }
        }
        private async Task UpdateUserLastActiveDate(User user){
            user.LastActive=DateTime.UtcNow;
            await SaveAll();
        }
        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// Decodes hashed password from provided user object, then it compares that password with the one provided in a login query
        /// If passwords match, returns true, otherwise returns false
        /// </summary>
        private bool VerifyUser(string password, User user)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt);
            using (hmac)
            {
                return ByteArrayCompare(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)), user.PasswordHash);
            }
        }
        /// <summary>
        /// Compares two byte arrays, if they match, it returns true, otherwise returns false
        /// </summary>
        private bool ByteArrayCompare(byte[] a1, byte[] a2)
        {

            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }
        /// <summary>
        /// Hashes users password and adds user to the database
        /// </summary>
        public async Task<User> RegisterUser(RegisterUserDTO userDTO)
        {
            var hashedBundle = HashPassword(userDTO.Password);
            User user = new User
            {
                Username = userDTO.Username,
                Email = userDTO.Email,
                PasswordHash = hashedBundle.PasswordHash,
                PasswordSalt = hashedBundle.PasswordSalt,
                CreatedDate=DateTime.UtcNow,
            };
            user.FavouriteAirport=await _context.Airports.FirstOrDefaultAsync(x=>x.Id==userDTO.FavouriteAirport.Id);
            await _context.Users.AddAsync(user);
            await SaveAll();
            return user;
        }
        /// <summary>
        /// Hashes password using SHA512 algorithm, returns tuple containing hashed password and its salt
        /// </summary>
        private (byte[] PasswordHash, byte[] PasswordSalt) HashPassword(string password)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            using (hmac)
            {
                return (hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),hmac.Key);
            }
        }
    }
}