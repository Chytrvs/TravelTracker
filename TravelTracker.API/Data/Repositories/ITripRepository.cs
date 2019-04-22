using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Data.Repositories
{
    public interface ITripRepository
    {
        Task<List<Trip>> GetUserTrips(string username);
        Task<Trip> GetTrip(int id);
        Task<Trip> AddTrip(Trip trip);
        Task<Flight> AddFlight(FlightDTO flightDTO);
        Task<Airport> AddAirport(Airport airport);
    }
}