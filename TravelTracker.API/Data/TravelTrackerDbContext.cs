using Microsoft.EntityFrameworkCore;
using TravelTracker.API.Data.DataModels;

namespace TravelTracker.API.Data
{
    public class TravelTrackerDbContext:DbContext
    {
        public DbSet<Point> Points {get;set;}
        public DbSet<User> Users{get;set;}
        public DbSet<Trip> Trips{get;set;}
        public DbSet<Flight> Flight{get;set;}
        public TravelTrackerDbContext(DbContextOptions<TravelTrackerDbContext> options):base(options)
        {
            
        }

    }
}