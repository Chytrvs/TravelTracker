using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Data.Repositories
{
    public interface IFlightRepository
    {
        Task<Flight> AddFlight(NewFlightDTO newFlightDTO);
        Task<Flight> GetFlight(int id);
        Task<bool> DeleteFlight(Flight flight);
        Task<Airport> AddAirport(Airport airport);
        Task<List<Airport>> GetAirports();
        Task<List<Flight>> GetUserFlights(int userId);
        Task<bool> SaveAll();
    }
}