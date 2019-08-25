using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data.Repositories;


namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _repository;

        public TripsController(ITripRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Adds flight provided in FlightRequestDto
        /// 
        /// Sample request:
        ///     POST
        ///     {
        ///       "Username":"login",
        ///       "DepartureAirportAcronym": "KABQ",
        ///       "DestinationAirportAcronym": "KALB",
        ///     }
        /// </summary>
        
        [HttpPost]
        
        public async Task<IActionResult> AddFlight(FlightRequestDTO flightRequestDTO)
        {
            var flight = await _repository.AddFlight(flightRequestDTO);
            if (flight != null)
            {
                return Ok(flight);
            }
            return BadRequest("One of the airports doesnt exist!");
        }
        /// <summary>
        /// Adds new airport
        /// 
        /// Sample request:
        ///     POST
        ///     {
        ///       "Name": "Philip S W Goldson International Airport",
        ///       "Acronym": "MZBZ",
        ///       "Latitude": "17.5386",
        ///       "Longitude": "-88.3042"
        ///     }
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddAirport(Airport airport)
        {
            Airport newairport = await _repository.AddAirport(airport);
            if (newairport != null)
            {
                return Ok(newairport);
            }
            return BadRequest("Airport already exists!");
        }
        /// <summary>
        /// Provides flights attached to specified user
        /// 
        /// Sample request:
        ///     GET
        ///     http://localhost:5000/api/Trips/GetUserFlights/login
        /// </summary>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserFlights(string username)
        {
            var res = await _repository.GetUserFlights(username);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Flights cannot be found.");

        }
        /// <summary>
        /// Provides all airports in database
        /// 
        /// Sample request:
        ///     GET
        ///     http://localhost:5000/api/Trips/GetAirports
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAirports()
        {
            var res = await _repository.GetAirports();
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest("Airports cannot be found.");

        }
    }
}