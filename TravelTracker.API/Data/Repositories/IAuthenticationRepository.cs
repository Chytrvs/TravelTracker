using System.Threading.Tasks;

namespace TravelTracker.API.Data.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<User> RegisterUser(string username, string password);
        Task<User> LoginUser(string username,string password);
        Task<bool> DoesUserExist(string username);
    }
}