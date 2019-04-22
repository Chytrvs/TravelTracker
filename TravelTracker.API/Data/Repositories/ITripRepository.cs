using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTracker.API.Data.DataModels;

namespace TravelTracker.API.Data.Repositories
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetUserTrips(string username);
        Task<Trip> GetTrip(int id);
        Task<Trip> AddTrip();

    }
}