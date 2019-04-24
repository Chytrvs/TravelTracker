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
        public async Task<Flight> AddFlight(FlightDTO flightDTO)
        {
            User user=await _userRepo.GetUser(flightDTO.Username);
            if(user==null)
                return null;

            Airport DepartureAirport = await _context.Airports.FirstOrDefaultAsync(x=>x.Acronym==flightDTO.DepartureAirportAcronym);
            if(DepartureAirport==null)
                return null;
                
            Airport DestinationAirport = await _context.Airports.FirstOrDefaultAsync(x=>x.Acronym==flightDTO.DestinationAirportAcronym);
            if(DestinationAirport==null)
                return null;
            
            Flight flight=new Flight();
            flight.FlightDepartureAirport=DepartureAirport;
            flight.FlightDestinationAirport=DestinationAirport;
            flight.User=user;
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return flight;
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

        public Task<List<Flight>> GetUsersFlights(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}