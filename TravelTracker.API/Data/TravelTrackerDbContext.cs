using Microsoft.EntityFrameworkCore;
using TravelTracker.API.Data.DataModels;

namespace TravelTracker.API.Data
{
    public class TravelTrackerDbContext:DbContext
    {
        public DbSet<User> Users{get;set;}
        public DbSet<Flight> Flights{get;set;}
        public DbSet<Airport> Airports { get; set; }
        public TravelTrackerDbContext(DbContextOptions<TravelTrackerDbContext> options):base(options)
        {
            
        }

    }
}