using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data.Repositories;
using AutoMapper;
using System.Security.Claims;

namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FlightController : ControllerBase
    {
        private readonly IFlightRepository _repository;
        private IMapper _mapper { get; }

        public FlightController(IFlightRepository repository, IMapper mapper)
        {
            _mapper = mapper;
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
        public async Task<IActionResult> AddFlight(NewFlightDTO newFlightDTO)
        {
            if(newFlightDTO.Username!=User.FindFirst(ClaimTypes.Name).Value)
            {
                return Unauthorized();
            }

            var flight = await _repository.AddFlight(newFlightDTO);
            if (flight != null)
            {
                var FlightToReturn=_mapper.Map<DetailedFlightDTO>(flight);
                return new JsonResult(FlightToReturn);
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
                return new JsonResult(newairport);
            }
            return BadRequest("Airport already exists!");
        }
        /// <summary>
        /// Provides flights attached to specified user
        /// 
        /// Sample request:
        ///     GET
        ///     http://localhost:5000/api/Flight/GetUserFlights/login
        /// </summary>
        [HttpGet("{username}")]
        public async Task<IActionResult> GetUserFlights(string username)
        {
            if(username!=User.FindFirst(ClaimTypes.Name).Value)
            {
                return Unauthorized();
            }
            
            IEnumerable<Flight> flights = await _repository.GetUserFlights(username);
            if (flights != null)
            {
                var FlightsToReturn= _mapper.Map<IEnumerable<FlightEndpointsDTO>>(flights);
                return new JsonResult(FlightsToReturn);
            }
            return BadRequest("Flights cannot be found.");

        }
        /// <summary>
        /// Provides all airports in database
        /// 
        /// Sample request:
        ///     GET
        ///     http://localhost:5000/api/Flight/GetAirports
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAirports()
        {
            var airports = await _repository.GetAirports();
            if (airports != null)
            {
                var AirportsToReturn=_mapper.Map<IEnumerable<AirportResponseDTO>>(airports);
                return new JsonResult(AirportsToReturn);
            }
            return BadRequest("Airports cannot be found.");

        }
    }
}