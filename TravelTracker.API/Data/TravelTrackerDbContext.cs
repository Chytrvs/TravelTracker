using Microsoft.EntityFrameworkCore;

namespace TravelTracker.API.Data
{
    public class TravelTrackerDbContext:DbContext
    {
        public DbSet<PointDataModel> Points {get;set;}
        public TravelTrackerDbContext(DbContextOptions<TravelTrackerDbContext> options):base(options)
        {
            
        }

    }
}