using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelTracker.API.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TravelTrackerDbContext _context;

        public UserRepository(TravelTrackerDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DoesUserExist(string username)
        {
            if (await _context.Users.AnyAsync(x => x.Username == username))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<User> GetUser(string username)
        {
           if(!await DoesUserExist(username))
           return null;
           return await _context.Users.FirstAsync(x=>x.Username==username);
        }
    }
}