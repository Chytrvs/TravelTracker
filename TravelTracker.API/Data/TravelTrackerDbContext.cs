using Microsoft.EntityFrameworkCore;

namespace TravelTracker.API.Data
{
    public class TravelTrackerDbContext:DbContext
    {
        public DbSet<Point> Points {get;set;}
        public DbSet<User> Users{get;set;}
        public TravelTrackerDbContext(DbContextOptions<TravelTrackerDbContext> options):base(options)
        {
            
        }

    }
}