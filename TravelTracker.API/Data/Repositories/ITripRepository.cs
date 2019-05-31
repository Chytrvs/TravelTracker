using System.Collections.Generic;
using System.Threading.Tasks;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Data.Repositories
{
    public interface ITripRepository
    {
        Task<FlightResponseDTO> AddFlight(FlightRequestDTO flightRequestDTO);
        Task<Airport> AddAirport(Airport airport);
        Task<List<AirportResponseDTO>> GetAirports();
        Task<List<FlightResponseDTO>> GetUserFlights(string username);
    }
}