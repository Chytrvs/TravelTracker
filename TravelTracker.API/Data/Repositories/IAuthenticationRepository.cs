using System.Threading.Tasks;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Data.Repositories
{
    public interface IAuthenticationRepository
    {
        Task<User> RegisterUser(RegisterUserDTO userDTO);
        Task<User> LoginUser(string username,string password);
    }
}