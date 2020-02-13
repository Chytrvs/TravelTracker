using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelTracker.API.Data.Repositories
{
    /// <summary>
    /// Implements db queries that provide info aboout users
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly TravelTrackerDbContext _context;

        public UserRepository(TravelTrackerDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Checks if user exists
        /// </summary>
        public async Task<bool> DoesUserExist(string username)
        {
            return await _context.Users.AnyAsync(x => x.Username == username);
        }
        /// <summary>
        /// Returns user with specified username
        /// </summary>
        public async Task<User> GetUser(string username)
        {
           if(!await DoesUserExist(username))
                return null;
           return await _context.Users.Include(x=>x.FavouriteAirport).FirstAsync(x=>x.Username==username);
        }
        public async Task<bool> IsEmailTaken(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }
    }
}