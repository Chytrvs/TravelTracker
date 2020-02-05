using TravelTracker.API.Data;

namespace TravelTracker.API.Helpers
{
    public interface IJWTTokenBuilder
    {
        string BuildJWTToken(User user);
    }
}