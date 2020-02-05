using System.Threading.Tasks;

namespace TravelTracker.API.Data.Repositories
{    public interface IUserRepository
    {
        Task<User> GetUser(string username);
        Task<bool> DoesUserExist(string username);
        Task<bool> IsEmailTaken(string email);
    }
}