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
    [Route("api/user/{userId}/")]
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

        [HttpPost("flight")]
        public async Task<IActionResult> AddFlight(NewFlightDTO newFlightDTO)
        {
            if (newFlightDTO.Username != User.FindFirst(ClaimTypes.Name).Value)
            {
                return Unauthorized();
            }

            var flight = await _repository.AddFlight(newFlightDTO);
            if (flight != null)
            {
                var FlightToReturn = _mapper.Map<DetailedFlightDTO>(flight);
                return new JsonResult(FlightToReturn);
            }
            return BadRequest("One of the airports doesnt exist!");
        }

        /// <summary>
        /// Provides flights attached to specified user
        /// 
        /// Sample request:
        ///     GET
        ///     http://localhost:5000/api/user/{userId}/flights
        /// </summary>
        [HttpGet("flights")]
        public async Task<IActionResult> GetUserFlights(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var flights = await _repository.GetUserFlights(userId);

            if (flights.Count==0)
            {
                return BadRequest("Flights cannot be found.");
            }

            var FlightsToReturn = _mapper.Map<IEnumerable<FlightEndpointsDTO>>(flights);
            return new JsonResult(FlightsToReturn);

        }
        /// <summary>
        /// Deletes specified flight
        /// 
        /// Sample request:
        ///     DELETE
        ///     http://localhost:5000/api/user/{userId}/flight/{flightID}
        /// </summary>
        [HttpDelete("flight/{flightID}")]
        public async Task<IActionResult> DeleteFlight(int userId, int flightID)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var flight=await _repository.GetFlight(flightID);

            if(flight==null)
                return NotFound($"Flight id: {flightID} doesnt exist");

            if(await _repository.DeleteFlight(flight))
                return Ok();

            return BadRequest("Failed to delete flight");
        }
        /// <summary>
        /// Returns specified flight
        /// 
        /// Sample request:
        ///     GET
        ///     http://localhost:5000/api/user/{userId}/flight/{flightID}
        /// </summary>
        [HttpGet("flight/{flightID}")]
        public async Task<IActionResult> GetUserFlight(int userId, int flightID)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var flight = await _repository.GetFlight(flightID);

            if (flight==null)
            {
                return BadRequest($"Flight id: {flightID} doesnt exist");
            }

            if(userId!=flight.UserId)
                return Unauthorized();

            var FlightToReturn = _mapper.Map<FlightEndpointsDTO>(flight);
            return new JsonResult(FlightToReturn);

        }

    }
}