using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TravelTracker.API.Data.Repositories
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly TravelTrackerDbContext _context;
        private struct HashedPasswordBundle
        {

            public byte[] PasswordHash { get; set; }
            public byte[] PasswordSalt { get; set; }
            public HashedPasswordBundle(byte[] passwordHash, byte[] passwordSalt)
            {
                PasswordHash = passwordHash;
                PasswordSalt = passwordSalt;
            }
        }
        public AuthenticationRepository(TravelTrackerDbContext context)
        {
            _context = context;
        }
        public Task<User> LoginUser(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> RegisterUser(string username, string password)
        {
            if (await DoesUserExist(username))
            {
                return null;
            }
            User user = new User();
            HashedPasswordBundle hashedBundle = HashPassword(password);
            user.PasswordHash = hashedBundle.PasswordHash;
            user.PasswordSalt = hashedBundle.PasswordSalt;
            user.Username = username;
            await _context.Users.AddAsync(user);
            return user;
        }

        private HashedPasswordBundle HashPassword(string password)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            using (hmac)
            {
                return new HashedPasswordBundle(
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                    hmac.Key
                );
            }
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
    }
}