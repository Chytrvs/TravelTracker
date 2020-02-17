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
            bool result = DateTime.TryParse(newFlightDTO.FlightDate,out FlightDateConverted);
            Flight flight = new Flight
            {
                DepartureAirport = departureAirport,
                DestinationAirport = destinationAirport,
                User = user,
                Description=newFlightDTO.Description,
                CreatedDate=DateTime.UtcNow,
                FlightDate=FlightDateConverted
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
            //Check if user exists
            if (!await _userRepo.DoesUserExist(username))
                return null;
            //Get user, his flights and airports attached to those flights
            var user = await _context.Users
            .Include(u => u.UserFlights)
                .ThenInclude(u => u.DepartureAirport)
            .Include(u => u.UserFlights)
                .ThenInclude(u => u.DestinationAirport)
            .FirstOrDefaultAsync(x => x.Username == username);
            //Check if user has any flights
            if (!user.UserFlights.Any())
                return null;
            //Create list of flights
            List<Flight> response = new List<Flight>();
            foreach (var flight in user.UserFlights)
            {
                response.Add(new Flight
                {
                    DepartureAirport = flight.DepartureAirport,
                    DestinationAirport = flight.DestinationAirport,
                    Description=flight.Description,
                    CreatedDate=flight.CreatedDate,
                    FlightDate=flight.FlightDate,
                    User=flight.User
                });
            }
            return response;
        }
        /// <summary>
        /// Provides list of all airports in database
        /// </summary>
        public async Task<List<Airport>> GetAirports()
        {
            List<Airport> response = new List<Airport>();
            var airports = await _context.Airports.ToListAsync();
            foreach (var airport in airports)
            {
                response.Add(new Airport
                {
                    Id=airport.Id,
                    Name = airport.Name,
                    Acronym = airport.Acronym,
                    Latitude=airport.Latitude,
                    Longitude=airport.Longitude
                });
            }
            return response;

        }

    }
}