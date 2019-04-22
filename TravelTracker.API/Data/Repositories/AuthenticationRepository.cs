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
        public async Task<User> LoginUser(string username, string password)
        {
            User user = await _context.Users.FirstOrDefaultAsync(x=>x.Username==username);
            if(user==null){
                return null;
            }
            if(!VerifyUser(username,password,user)){
                return null;
            }
            return user;
        }

        private bool VerifyUser(string username, string password, User user)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt);
            using (hmac)
            {
                    return ByteArrayCompare(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),user.PasswordHash);
            }
        }
        private bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i=0; i<a1.Length; i++)
                if (a1[i]!=a2[i])
                    return false;

            return true;
        }

        public async Task<User> RegisterUser(string username, string password,string email)
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
            user.Email=email;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
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