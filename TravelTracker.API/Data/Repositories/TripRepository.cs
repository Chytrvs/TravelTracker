using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;

namespace TravelTracker.API.Data.Repositories
{
    /// <summary>
    /// Implements db queries that provide data about flights and airports
    /// </summary>
    public class TripRepository : ITripRepository
    {
        private readonly TravelTrackerDbContext _context;
        private readonly IUserRepository _userRepo;


        public TripRepository(TravelTrackerDbContext Context, IUserRepository userRepo)
        {
            _context = Context;
            _userRepo = userRepo;
        }
        /// <summary>
        /// Adds new flight to database
        /// </summary>
        public async Task<FlightResponseDTO> AddFlight(FlightRequestDTO flightRequestDTO)
        {
            //Gets user with specified username
            User user = await _userRepo.GetUser(flightRequestDTO.Username);
            if (user == null)
                return null;
            //Gets departure airport with specified acronym
            Airport departureAirport = await _context.Airports.FirstOrDefaultAsync(x => x.Acronym == flightRequestDTO.DepartureAirportAcronym);
            if (departureAirport == null)
                return null;
            //Gets destination airport with specified acronym
            Airport destinationAirport = await _context.Airports.FirstOrDefaultAsync(x => x.Acronym == flightRequestDTO.DestinationAirportAcronym);
            if (destinationAirport == null)
                return null;
            //Creates new flight with data provided from db
            Flight flight = new Flight
            {
                FlightDepartureAirport = departureAirport,
                FlightDestinationAirport = destinationAirport,
                User = user
            };
            //Adds new flight to the database
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
            return new FlightResponseDTO
            {
                Username = user.Username,
                DepartureAirport = departureAirport,
                DestinationAirport = destinationAirport
            };
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
        public async Task<List<FlightResponseDTO>> GetUserFlights(string username)
        {
            //Check if user exists
            if (!await _userRepo.DoesUserExist(username))
                return null;
            //Get user, his flights and airports attached to those flights
            var user = await _context.Users
            .Include(u => u.UserFlights)
                .ThenInclude(u => u.FlightDepartureAirport)
            .Include(u => u.UserFlights)
                .ThenInclude(u => u.FlightDestinationAirport)
            .FirstOrDefaultAsync(x => x.Username == username);
            //Check if user has any flights
            if (!user.UserFlights.Any())
                return null;
            //Create list of flights
            List<FlightResponseDTO> response = new List<FlightResponseDTO>();
            foreach (var flight in user.UserFlights)
            {
                response.Add(new FlightResponseDTO
                {
                    Username = user.Username,
                    DepartureAirport = flight.FlightDepartureAirport,
                    DestinationAirport = flight.FlightDestinationAirport
                });
            }
            return response;
        }
        /// <summary>
        /// Provides list of all airports in database
        /// </summary>
        public async Task<List<AirportResponseDTO>> GetAirports()
        {
            List<AirportResponseDTO> response = new List<AirportResponseDTO>();
            var airports = await _context.Airports.ToListAsync();
            foreach (var airport in airports)
            {
                response.Add(new AirportResponseDTO
                {
                    Name = airport.Name,
                    Acronym = airport.Acronym

                });
            }
            return response;

        }

    }
}