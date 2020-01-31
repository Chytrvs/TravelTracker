using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Data.Repositories
{
    public interface ITripRepository
    {
        Task<Flight> AddFlight(NewFlightDTO newFlightDTO);
        Task<Airport> AddAirport(Airport airport);
        Task<List<Airport>> GetAirports();
        Task<List<Flight>> GetUserFlights(string username);
    }
}