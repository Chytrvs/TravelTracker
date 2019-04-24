using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Data.Repositories
{
    public interface ITripRepository
    {
        Task<Flight> AddFlight(FlightDTO flightDTO);
        Task<Airport> AddAirport(Airport airport);
        Task<List<Flight>> GetUsersFlights(string username);
    }
}