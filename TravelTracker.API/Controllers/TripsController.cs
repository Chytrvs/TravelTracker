using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelTracker.API.Data;
using TravelTracker.API.Data.DataModels;
using TravelTracker.API.Data.DataTransferObjects;
using TravelTracker.API.Data.Repositories;


namespace TravelTracker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ITripRepository _repository;

        public TripsController(ITripRepository repository)
        {
            _repository = repository;
        }
        public async Task<IActionResult> AddFlight(FlightRequestDTO flightRequestDTO)
        {
            var flight = await _repository.AddFlight(flightRequestDTO);
            if (flight != null)
            {
                return Ok(flight);
            }
            return BadRequest("One of the airports doesnt exist!");
        }
        public async Task<IActionResult> AddAirport(Airport airport)
        {
            Airport newairport = await _repository.AddAirport(airport);
            if (newairport != null)
            {
                return Ok(newairport);
            }
            return BadRequest("Airport already exists!");
        }
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
        [HttpGet]
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