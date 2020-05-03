using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;
using System;

namespace TravelTracker.API.Data.Repositories
{
    /// <summary>
    /// Implements db queries that provide data about flights and airports
    /// </summary>
    public class FlightRepository : IFlightRepository
    {
        private readonly TravelTrackerDbContext _context;
        private readonly IUserRepository _userRepo;


        public FlightRepository(TravelTrackerDbContext Context, IUserRepository userRepo)
        {
            _context = Context;
            _userRepo = userRepo;
        }
        /// <summary>
        /// Adds new flight to database
        /// </summary>
        public async Task<Flight> AddFlight(NewFlightDTO newFlightDTO)
        {
            //Gets user with specified username
            User user = await _userRepo.GetUser(newFlightDTO.Username);
            if (user == null)
                return null;

            //Gets departure airport with specified acronym
            Airport departureAirport = await _context.Airports.FirstOrDefaultAsync(x => x.Acronym == newFlightDTO.DepartureAirportAcronym);
            if (departureAirport == null)
                return null;

            //Gets destination airport with specified acronym
            Airport destinationAirport = await _context.Airports.FirstOrDefaultAsync(x => x.Acronym == newFlightDTO.DestinationAirportAcronym);
            if (destinationAirport == null)
                return null;

            //Creates new flight with data provided from db
            DateTime FlightDateConverted;
            bool isDateParsedSuccessfuly = DateTime.TryParse(newFlightDTO.FlightDate,out FlightDateConverted);
            Flight flight = new Flight
            {
                DepartureAirport = departureAirport,
                DestinationAirport = destinationAirport,
                User = user,
                Description=newFlightDTO.Description,
                CreatedDate=DateTime.UtcNow,
                FlightDate=isDateParsedSuccessfuly?FlightDateConverted:DateTime.UtcNow
            };
            
            //Adds new flight to the database
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return flight;
        }

        /// <summary>
        /// Adds new airport to database
        /// </summary>
        public async Task<Airport> AddAirport(Airport airport)
        {
            //Check if airport already exists
            if (await _context.Airports.FirstOrDefaultAsync(x => x.Acronym == airport.Acronym) != null)
            {
                return null;
            }
            //Add airport to database
            await _context.Airports.AddAsync(airport);
            await _context.SaveChangesAsync();
            return airport;
        }
        /// <summary>
        /// Provides list of flights that are attached to specific user
        /// </summary>
        public async Task<List<Flight>> GetUserFlights(string username)
        {
            //find flights attached to given username, and then include destination and departure airports to them
            return await _context.Flights.Where(x=>x.User.Username==username)
                                         .Include(x=>x.DepartureAirport)
                                         .Include(x=>x.DestinationAirport)
                                         .ToListAsync();
        }
        
        /// <summary>
        /// Provides list of all airports in database
        /// </summary>
        public async Task<List<Airport>> GetAirports()
        {
            return await _context.Airports.ToListAsync();
        }

    }
}