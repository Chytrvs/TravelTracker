using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTracker.API.Data.DataModels;

namespace TravelTracker.API.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TravelTrackerDbContext _context;

        public TripRepository(TravelTrackerDbContext Context)
        {
            _context = Context;
        }

        public Task<Trip> AddTrip()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Trip> GetTrip(int id)
        {
            return await _context.Trips.FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<List<Trip>> GetUserTrips(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}