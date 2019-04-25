using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Data.Repositories
{
    public class TripRepository : ITripRepository
    {
        private readonly TravelTrackerDbContext _context;
        private readonly IUserRepository _userRepo;

        public TripRepository(TravelTrackerDbContext Context,IUserRepository userRepo)
        {
            _context = Context;
            _userRepo = userRepo;
        }
        public async Task<FlightResponseDTO> AddFlight(FlightRequestDTO flightRequestDTO)
        {
            User user=await _userRepo.GetUser(flightRequestDTO.Username);
            if(user==null)
                return null;

            Airport departureAirport = await _context.Airports.FirstOrDefaultAsync(x=>x.Acronym==flightRequestDTO.DepartureAirportAcronym);
            if(departureAirport==null)
                return null;
                
            Airport destinationAirport = await _context.Airports.FirstOrDefaultAsync(x=>x.Acronym==flightRequestDTO.DestinationAirportAcronym);
            if(destinationAirport==null)
                return null;
            
            Flight flight=new Flight();
            flight.FlightDepartureAirport=departureAirport;
            flight.FlightDestinationAirport=destinationAirport;
            flight.User=user;
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return new FlightResponseDTO
            {
                Username=user.Username,
                DepartureAirport=departureAirport,
                DestinationAirport=destinationAirport
            };
        }

        public async Task<Airport> AddAirport(Airport airport)
        {
            if(await _context.Airports.FirstOrDefaultAsync(x=>x.Acronym==airport.Acronym)!=null){
                return null;
            }
            await _context.Airports.AddAsync(airport);
            await _context.SaveChangesAsync();
            return airport;
        }

        public async Task<List<FlightResponseDTO>> GetUserFlights(string username)
        {
            if(!await _userRepo.DoesUserExist(username))
            return null;
            
            var user=await _context.Users
            .Include(u=>u.UserFlights)
                .ThenInclude(u=>u.FlightDepartureAirport)
            .Include(u=>u.UserFlights)
                .ThenInclude(u=>u.FlightDestinationAirport)
            .FirstOrDefaultAsync(x=>x.Username==username);
            if(!user.UserFlights.Any())
            return null;
            List<FlightResponseDTO> response=new List<FlightResponseDTO>();
            foreach (var flight in user.UserFlights)
            {
                response.Add(new FlightResponseDTO{
                    Username=user.Username,
                    DepartureAirport=flight.FlightDepartureAirport,
                    DestinationAirport=flight.FlightDestinationAirport
                });
            }
            return response;
        }
    }
}